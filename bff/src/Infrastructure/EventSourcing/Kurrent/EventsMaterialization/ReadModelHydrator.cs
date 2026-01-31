namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

public sealed class ReadModelHydrator(
    KurrentDBPersistentSubscriptionsClient client,
    IProjectionWorkerFactory projectionWorkerFactory) : IReadModelHydrator
{
    public async Task Subscribe(CancellationToken cancellationToken)
    {
        await using var subscription = client.SubscribeToAll(
            "read-model-hydrator-subscription",
            cancellationToken: cancellationToken);
        await foreach (var message in subscription.Messages)
        {
            Console.WriteLine($"Received message '{message.GetType()}'.");
            Task action = message switch
            {
                PersistentSubscriptionMessage.SubscriptionConfirmation c => Task.Run(() => Console.WriteLine($"Subscription to all confirmed with id: {subscription.SubscriptionId}"), cancellationToken),
                PersistentSubscriptionMessage.Event e => this.HandleEvent(subscription, e),
                _ => Task.CompletedTask
            };

            await action;
        }
    }

    private async Task HandleEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult subscription,
        PersistentSubscriptionMessage.Event subscriptionEvent)
    {
        var resolvedEvent = subscriptionEvent.ResolvedEvent;
        try
        {
            var metadata = GetMetadata(resolvedEvent);
            if (string.IsNullOrWhiteSpace(metadata.EventTypeName))
            {
                await subscription.Ack([resolvedEvent]);
                return;
            }

            Console.WriteLine("Handling event of type: " + metadata.EventTypeName);
            var aggregateEvent = await GetEvent(subscription, resolvedEvent, metadata.EventTypeName);
            if (aggregateEvent is null)
            {
                Console.WriteLine("Failed to deserialize event of type: " + metadata.EventTypeName);
                return;
            }

            Console.WriteLine($"Handling event {aggregateEvent}");

            await this.FeedProjections(aggregateEvent, CancellationToken.None);

            await subscription.Ack([resolvedEvent]);
        }
        catch
        {
            await subscription.Nack(
                PersistentSubscriptionNakEventAction.Park,
                "Error handling event",
                [resolvedEvent]);
            // Add some logging here.
        }
    }

    private async Task FeedProjections(IAggregateEvent aggregateEvent, CancellationToken cancellationToken)
    {
        var workers = projectionWorkerFactory.Create(aggregateEvent.GetType());
        var work = workers.Select(workers => workers.Materialize(aggregateEvent, cancellationToken))
            .ToList();
        await Task.WhenAll(work);
    }

    private static async Task<IAggregateEvent?> GetEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult subscription,
        ResolvedEvent resolvedEvent,
        string eventTypeName)
    {
        var eventType = Type.GetType(eventTypeName);
        if (eventType is null)
        {
            Console.WriteLine("Unknown event type: " + eventTypeName);
            await subscription.Ack([resolvedEvent]);
            return null;
        }

        var eventDataString = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
        return JsonSerializer.Deserialize(
            eventDataString,
            eventType,
            JsonSerializerOptionsFactory.GetOptions()) as IAggregateEvent;
    }

    private static EventMetadata GetMetadata(ResolvedEvent resolvedEvent)
    {
        var metadataString = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.ToArray());
        var metadata = JsonSerializer.Deserialize<EventMetadata>(
            metadataString,
            JsonSerializerOptionsFactory.GetOptions());
        return metadata;
    }
}

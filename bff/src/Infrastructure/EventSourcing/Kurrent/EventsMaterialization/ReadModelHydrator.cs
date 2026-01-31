namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using Microsoft.Extensions.DependencyInjection;

public sealed class ReadModelHydrator(
    KurrentDBPersistentSubscriptionsClient client,
    IServiceProvider serviceProvider) : IReadModelHydrator
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
                PersistentSubscriptionMessage.SubscriptionConfirmation c => Task.Run(() => Console.WriteLine($"Subscription to all confirmed with id: {subscription.SubscriptionId}")),
                PersistentSubscriptionMessage.Event e => this.HandleEvent(subscription, e.ResolvedEvent, cancellationToken),
                _ => Task.CompletedTask
            };

            await action;
        }
    }

    private async Task HandleEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult subscription,
        ResolvedEvent resolvedEvent,
        CancellationToken cancellationToken)
    {
        try
        {
            var metadata = GetMetadata(resolvedEvent);
            if (string.IsNullOrWhiteSpace(metadata.EventTypeName))
            {
                await subscription.Ack([resolvedEvent]);
                return;
            }

            Console.WriteLine("Handling event of type: " + metadata.EventTypeName);
            var @event = await GetEvent(subscription, resolvedEvent, metadata.EventTypeName);
            if (@event is null)
            {
                Console.WriteLine("Failed to deserialize event of type: " + metadata.EventTypeName);
                return;
            }

            Console.WriteLine($"Handling event {@event}");
            var materializer = serviceProvider.GetRequiredKeyedService<IEventMaterializer>(metadata.EventTypeName);
            await materializer.Materialize(@event, cancellationToken);
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

    private static async Task<object?> GetEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult subscription,
        ResolvedEvent resolvedEvent,
        string eventTypeName)
    {
        var eventType = Type.GetType(eventTypeName);
        if (eventType is null)
        {
            Console.WriteLine("Unknown event type: " + eventTypeName);
            await subscription.Ack([resolvedEvent]);
            return false;
        }

        var eventDataString = System.Text.Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
        return System.Text.Json.JsonSerializer.Deserialize(
            eventDataString,
            eventType,
            Infrastructure.Serialization.Json.JsonSerializerOptionsFactory.GetOptions());
    }

    private static EventMetadata GetMetadata(ResolvedEvent resolvedEvent)
    {
        var metadataString = System.Text.Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.ToArray());
        var metadata = System.Text.Json.JsonSerializer.Deserialize<EventMetadata>(
            metadataString,
            Infrastructure.Serialization.Json.JsonSerializerOptionsFactory.GetOptions());
        return metadata;
    }
}

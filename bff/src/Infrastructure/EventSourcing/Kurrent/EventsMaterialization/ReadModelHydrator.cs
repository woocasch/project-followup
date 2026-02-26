namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Infrastructure.Serialization.Json;

public sealed class ReadModelHydrator(
    KurrentDBPersistentSubscriptionsClient client,
    IProjectionWorkerFactory projectionWorkerFactory,
    ILogger<ReadModelHydrator> logger) : IReadModelHydrator
{
    public async Task Subscribe(CancellationToken cancellationToken)
    {
        logger.SubscribeStarted();
        await using var subscription = client.SubscribeToAll(
            "read-model-hydrator-subscription",
            cancellationToken: cancellationToken);
        logger.SubscribeSubscriptionCreated();
        try
        {
            await foreach (var message in subscription.Messages)
            {
                logger.SubscribeMessageReceived(message.GetType());
                Task action = message switch
                {
                    PersistentSubscriptionMessage.SubscriptionConfirmation e => this.HandleSubscriptionConfirmation(subscription, e),
                    PersistentSubscriptionMessage.Event e => this.HandleEvent(subscription, e),
                    _ => this.HandleUnknownEvent(subscription, message),
                };

                logger.SubscribeExecutingHandler(message.GetType()); ;
                await action;
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            logger.SubscribeApplicationShutdown();
        }
        catch (Exception ex)
        {
            logger.SubscribeFatalError(ex);
            throw;
        }
    }

    private async Task HandleSubscriptionConfirmation(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult _,
        PersistentSubscriptionMessage.SubscriptionConfirmation confirmation)
    {
        logger.ExecutingSubscriptionConfirmationHandler(confirmation.GetType());
        await Task.Yield();
    }

    private async Task HandleUnknownEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult _,
        PersistentSubscriptionMessage message)
    {
        logger.ExecutingUnknownEventHandler(message.GetType());
        await Task.Yield();
    }

    private async Task HandleEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult subscription,
        PersistentSubscriptionMessage.Event subscriptionEvent)
    {
        logger.HandleEventStarted(subscriptionEvent.ResolvedEvent.OriginalStreamId);
        var resolvedEvent = subscriptionEvent.ResolvedEvent;
        try
        {
            logger.HandleEventReadingMetadata(subscriptionEvent.ResolvedEvent.OriginalStreamId);
            var rawMetadata = GetMetadata(resolvedEvent);
            if (string.IsNullOrWhiteSpace(rawMetadata?.EventTypeName))
            {
                logger.HandleEventNoMetadataFound(subscriptionEvent.ResolvedEvent.OriginalStreamId);
                await subscription.Ack([resolvedEvent]);
                return;
            }

            var metadata = rawMetadata.Value;
            logger.HandleEventDeserializingEvent(subscriptionEvent.ResolvedEvent.OriginalStreamId, metadata.EventTypeName);
            var aggregateEvent = await GetEvent(resolvedEvent, metadata.EventTypeName);
            if (aggregateEvent is null)
            {
                logger.HandleEventNotDeserializableEvent(subscriptionEvent.ResolvedEvent.OriginalStreamId, metadata.EventTypeName);
                await subscription.Nack(PersistentSubscriptionNakEventAction.Unknown, "Failed to deserialize event", [resolvedEvent]);
                return;
            }

            logger.HandleEventSendingToProjectionWorkers(subscriptionEvent.ResolvedEvent.OriginalStreamId, metadata.EventTypeName);

            await this.FeedProjections(resolvedEvent.OriginalStreamId, aggregateEvent, CancellationToken.None);

            await subscription.Ack([resolvedEvent]);
            logger.HandleEventCompleted(subscriptionEvent.ResolvedEvent.OriginalStreamId);
        }
        catch (Exception ex)
        {
            logger.HandleEventException(subscriptionEvent.ResolvedEvent.OriginalStreamId, ex);
            await subscription.Nack(
                PersistentSubscriptionNakEventAction.Park,
                "Error handling event",
                [resolvedEvent]);
        }
    }

    private async Task FeedProjections(string streamId, IAggregateEvent aggregateEvent, CancellationToken cancellationToken)
    {
        logger.FeedProjectionsStarted(streamId, aggregateEvent.GetType());
        var workers = projectionWorkerFactory.Create(aggregateEvent.GetType())
            .ToList();
        logger.FeedProjectionsWorkersRetrieved(streamId, aggregateEvent.GetType(), workers.Count);
        var work = workers.Select(workers => workers.Materialize(aggregateEvent, cancellationToken))
            .ToList();
        await Task.WhenAll(work);
        logger.FeedProjectionsCompleted(streamId);
    }

    private async Task<IAggregateEvent?> GetEvent(
        ResolvedEvent resolvedEvent,
        string eventTypeName)
    {
        var eventType = Type.GetType(eventTypeName);
        if (eventType is null)
        {
            logger.GetEventNullEventType(eventTypeName);
            return null;
        }

        var eventDataString = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
        return JsonSerializer.Deserialize(
            eventDataString,
            eventType,
            JsonSerializerOptionsFactory.GetOptions()) as IAggregateEvent;
    }

    private EventMetadata? GetMetadata(ResolvedEvent resolvedEvent)
    {
        var metadataString = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.ToArray());
        if (string.IsNullOrWhiteSpace(metadataString))
        {
            logger.GetMetadataNullMetadata(resolvedEvent.OriginalStreamId);
            return null;
        }

        var metadata = JsonSerializer.Deserialize<EventMetadata>(
            metadataString,
            JsonSerializerOptionsFactory.GetOptions());
        return metadata;
    }
}

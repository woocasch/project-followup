namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using Microsoft.Extensions.Logging;

internal static partial class ReadModelHydratorLogs
{
    [LoggerMessage(
        EventId = EventIds.SubscribeStarted,
        EventName = nameof(EventIds.SubscribeStarted),
        Level = LogLevel.Trace,
        Message = "Subscription to event stream started.")]
    public static partial void SubscribeStarted(
        this ILogger<ReadModelHydrator> logger);

    [LoggerMessage(
        EventId = EventIds.SubscribeSubscriptionCreated,
        EventName = nameof(EventIds.SubscribeSubscriptionCreated),
        Level = LogLevel.Trace,
        Message = "Subscription to event stream created.")]
    public static partial void SubscribeSubscriptionCreated(
        this ILogger<ReadModelHydrator> logger);

    [LoggerMessage(
    EventId = EventIds.SubscribeMessageReceived,
    EventName = nameof(EventIds.SubscribeMessageReceived),
    Level = LogLevel.Debug,
    Message = "Message of type '{MessageType}' received on subscription.")]
    public static partial void SubscribeMessageReceived(
        this ILogger<ReadModelHydrator> logger,
        Type messageType);

    [LoggerMessage(
        EventId = EventIds.SubscribeExecutingHandler,
        EventName = nameof(EventIds.SubscribeExecutingHandler),
        Level = LogLevel.Debug,
        Message = "Executing handler for message of type '{MessageType}' received on subscription.")]
    public static partial void SubscribeExecutingHandler(
        this ILogger<ReadModelHydrator> logger,
        Type messageType);

    [LoggerMessage(
        EventId = EventIds.SubscribeApplicationShutdown,
        EventName = nameof(EventIds.SubscribeApplicationShutdown),
        Level = LogLevel.Information,
        Message = "Application is shutting down, subscription to event stream will be stopped.")]
    public static partial void SubscribeApplicationShutdown(
        this ILogger<ReadModelHydrator> logger);

    [LoggerMessage(
        EventId = EventIds.SubscribeFatalError,
        EventName = nameof(EventIds.SubscribeFatalError),
        Level = LogLevel.Critical,
        Message = "Fatal error occurred in subscription to event stream.")]
    public static partial void SubscribeFatalError(
        this ILogger<ReadModelHydrator> logger,
        Exception exception);

    [LoggerMessage(
        EventId = EventIds.SubscribeCompleted,
        EventName = nameof(EventIds.SubscribeCompleted),
        Level = LogLevel.Trace,
        Message = "Subscription to event stream completed.")]
    public static partial void SubscribeCompleted(
        this ILogger<ReadModelHydrator> logger);

    [LoggerMessage(
        EventId = EventIds.ExecutingSubscriptionConfirmationHandler,
        EventName = nameof(EventIds.ExecutingSubscriptionConfirmationHandler),
        Level = LogLevel.Debug,
        Message = "Executing subscription confirmation handler for message of type '{MessageType}' received on subscription.")]
    public static partial void ExecutingSubscriptionConfirmationHandler(
        this ILogger<ReadModelHydrator> logger,
        Type messageType);

    [LoggerMessage(
        EventId = EventIds.ExecutingUnknownEventHandler,
        EventName = nameof(EventIds.ExecutingUnknownEventHandler),
        Level = LogLevel.Debug,
        Message = "Executing unknown event handler for message of type '{MessageType}' received on subscription.")]
    public static partial void ExecutingUnknownEventHandler(
        this ILogger<ReadModelHydrator> logger,
        Type messageType);

    [LoggerMessage(
        EventId = EventIds.HandleEventStarted,
        EventName = nameof(EventIds.HandleEventStarted),
        Level = LogLevel.Trace,
        Message = "Handling event from '{StreamId}' stream started.")]
    public static partial void HandleEventStarted(
        this ILogger<ReadModelHydrator> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.HandleEventReadingMetadata,
        EventName = nameof(EventIds.HandleEventReadingMetadata),
        Level = LogLevel.Trace,
        Message = "Reading metadata for event from '{StreamId}' stream.")]
    public static partial void HandleEventReadingMetadata(
        this ILogger<ReadModelHydrator> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.HandleEventNoMetadataFound,
        EventName = nameof(EventIds.HandleEventNoMetadataFound),
        Level = LogLevel.Warning,
        Message = "No metadata found for event from '{StreamId}' stream.")]
    public static partial void HandleEventNoMetadataFound(
        this ILogger<ReadModelHydrator> logger,
        string streamId);

    [LoggerMessage(
    EventId = EventIds.HandleEventDeserializingEvent,
    EventName = nameof(EventIds.HandleEventDeserializingEvent),
    Level = LogLevel.Trace,
    Message = "Deserializing event from '{StreamId}' stream of type '{EventTypeName}'.")]
    public static partial void HandleEventDeserializingEvent(
        this ILogger<ReadModelHydrator> logger,
        string streamId,
        string eventTypeName);

    [LoggerMessage(
        EventId = EventIds.HandleEventNotDeserializableEvent,
        EventName = nameof(EventIds.HandleEventNotDeserializableEvent),
        Level = LogLevel.Warning,
        Message = "Event from '{StreamId}' stream of type '{EventTypeName}' could not be deserialized.")]
    public static partial void HandleEventNotDeserializableEvent(
        this ILogger<ReadModelHydrator> logger,
        string streamId,
        string eventTypeName);

    [LoggerMessage(
        EventId = EventIds.HandleEventSendingToProjectionWorkers,
        EventName = nameof(EventIds.HandleEventSendingToProjectionWorkers),
        Level = LogLevel.Debug,
        Message = "Sending event from '{StreamId}' stream of type '{EventTypeName}' to projection workers.")]
    public static partial void HandleEventSendingToProjectionWorkers(
        this ILogger<ReadModelHydrator> logger,
        string streamId,
        string eventTypeName);

    [LoggerMessage(
        EventId = EventIds.HandleEventCompleted,
        EventName = nameof(EventIds.HandleEventCompleted),
        Level = LogLevel.Trace,
        Message = "Handling event from '{StreamId}' stream completed.")]
    public static partial void HandleEventCompleted(
        this ILogger<ReadModelHydrator> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.HandleEventException,
        EventName = nameof(EventIds.HandleEventException),
        Level = LogLevel.Error,
        Message = "Exception occurred while handling event from '{StreamId}' stream.")]
    public static partial void HandleEventException(
        this ILogger<ReadModelHydrator> logger,
        string streamId,
        Exception exception);

    [LoggerMessage(
        EventId = EventIds.FeedProjectionsStarted,
        EventName = nameof(EventIds.FeedProjectionsStarted),
        Level = LogLevel.Trace,
        Message = "Feeding projections for for event type '{EventType}' from stream {StreamId}.")]
    public static partial void FeedProjectionsStarted(
        this ILogger<ReadModelHydrator> logger,
        string streamId,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.FeedProjectionsWorkersRetrieved,
        EventName = nameof(EventIds.FeedProjectionsWorkersRetrieved),
        Level = LogLevel.Debug,
        Message = "Retrieved {WorkerCount} projection workers for event type '{EventType}' from stream {StreamId}.")]
    public static partial void FeedProjectionsWorkersRetrieved(
        this ILogger<ReadModelHydrator> logger,
        string streamId,
        Type eventType,
        int workerCount);

    [LoggerMessage(
        EventId = EventIds.FeedProjectionsCompleted,
        EventName = nameof(EventIds.FeedProjectionsCompleted),
        Level = LogLevel.Trace,
        Message = "Feeding projections for event from '{StreamId}' stream completed.")]
    public static partial void FeedProjectionsCompleted(
        this ILogger<ReadModelHydrator> logger,
        string streamId);

    [LoggerMessage(
        EventId = EventIds.GetEventNullEventType,
        EventName = nameof(EventIds.GetEventNullEventType),
        Level = LogLevel.Warning,
        Message = "Event type retrieved is null or empty ('{EventTypeName}').")]
    public static partial void GetEventNullEventType(
        this ILogger<ReadModelHydrator> logger,
        string eventTypeName);

    [LoggerMessage(
        EventId = EventIds.GetMetadataNullMetadata,
        EventName = nameof(EventIds.GetMetadataNullMetadata),
        Level = LogLevel.Warning,
        Message = "Metadata is null for stream {StreamId}.")]
    public static partial void GetMetadataNullMetadata(
        this ILogger<ReadModelHydrator> logger,
        string streamId);

    private static class EventIds
    {
        public const int SubscribeStarted = 1;

        public const int SubscribeSubscriptionCreated = 2;

        public const int SubscribeMessageReceived = 3;

        public const int SubscribeExecutingHandler = 4;

        public const int SubscribeApplicationShutdown = 5;

        public const int SubscribeFatalError = 6;

        public const int SubscribeCompleted = 7;

        public const int ExecutingSubscriptionConfirmationHandler = 8;

        public const int ExecutingUnknownEventHandler = 9;

        public const int HandleEventStarted = 10;

        public const int HandleEventReadingMetadata = 11;

        public const int HandleEventNoMetadataFound = 12;

        public const int HandleEventDeserializingEvent = 13;

        public const int HandleEventNotDeserializableEvent = 14;

        public const int HandleEventSendingToProjectionWorkers = 15;

        public const int HandleEventCompleted = 16;

        public const int HandleEventException = 17;

        public const int FeedProjectionsStarted = 18;

        public const int FeedProjectionsWorkersRetrieved = 19;

        public const int FeedProjectionsCompleted = 20;

        public const int GetEventNullEventType = 21;

        public const int GetMetadataNullMetadata = 22;
    }
}

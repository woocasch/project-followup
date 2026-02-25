namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.Logging;

internal static partial class EventPublisherLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Starting publishing event '{EventType}'.")]
    public static partial void Started(
        this ILogger<EventPublisher> logger,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.ConnectionOpened,
        EventName = nameof(EventIds.ConnectionOpened),
        Level = LogLevel.Trace,
        Message = "Connection to the event bus opened for event '{EventType}'.")]
    public static partial void ConnectionOpened(
        this ILogger<EventPublisher> logger,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.DataPrepared,
        EventName = nameof(EventIds.DataPrepared),
        Level = LogLevel.Trace,
        Message = "Event data prepared for event '{EventType}'.")]
    public static partial void DataPrepared(
        this ILogger<EventPublisher> logger,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.ExchangeNameNotConfigured,
        EventName = nameof(EventIds.ExchangeNameNotConfigured),
        Level = LogLevel.Error,
        Message = "Exchange name not configured for event '{EventType}'.")]
    public static partial void ExchangeNameNotConfigured(
        this ILogger<EventPublisher> logger,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.PublishingMessage,
        EventName = nameof(EventIds.PublishingMessage),
        Level = LogLevel.Debug,
        Message = "Publishing event '{EventType}' to exchange '{ExchangeName}'.")]
    public static partial void PublishingMessage(
        this ILogger<EventPublisher> logger,
        Type eventType,
        string exchangeName);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed publishing event '{EventType}'.")]
    public static partial void Completed(
        this ILogger<EventPublisher> logger,
        Type eventType);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ConnectionOpened = 2;

        public const int DataPrepared = 3;

        public const int ExchangeNameNotConfigured = 4;

        public const int PublishingMessage = 5;

        public const int Completed = 6;
    }
}

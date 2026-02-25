namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.Logging;

internal static partial class NamesMappingsLogs
{
    [LoggerMessage(
        EventId = EventIds.GetExchangeNameStarted,
        EventName = nameof(EventIds.GetExchangeNameStarted),
        Level = LogLevel.Trace,
        Message = "Getting exchange name for event {EventType} started.")]
    public static partial void GetExchangeNameStarted(
        this ILogger<NamesMappings> logger,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.NoExchangeMappingFound,
        EventName = nameof(EventIds.NoExchangeMappingFound),
        Level = LogLevel.Error,
        Message = "No exchange mapping found for event type {EventType}.")]
    public static partial void NoExchangeMappingFound(
        this ILogger<NamesMappings> logger,
        Type eventType);

    [LoggerMessage(
        EventId = EventIds.GetExchangeNameCompleted,
        EventName = nameof(EventIds.GetExchangeNameCompleted),
        Level = LogLevel.Trace,
        Message = "Getting exchange name for event {EventType} completed. Exchange name: {ExchangeName}.")]
    public static partial void GetExchangeNameCompleted(
        this ILogger<NamesMappings> logger,
        Type eventType,
        string exchangeName);

    [LoggerMessage(
        EventId = EventIds.GetQueueNameStarted,
        EventName = nameof(EventIds.GetQueueNameStarted),
        Level = LogLevel.Trace,
        Message = "Getting queue name for consumer {ConsumerType} started.")]
    public static partial void GetQueueNameStarted(
        this ILogger<NamesMappings> logger,
        Type consumerType);

    [LoggerMessage(
        EventId = EventIds.NoQueueMappingFound,
        EventName = nameof(EventIds.NoQueueMappingFound),
        Level = LogLevel.Error,
        Message = "No queue mapping found for consumer type {ConsumerType}.")]
    public static partial void NoQueueMappingFound(
        this ILogger<NamesMappings> logger,
        Type consumerType);

    [LoggerMessage(
        EventId = EventIds.GetQueueNameCompleted,
        EventName = nameof(EventIds.GetQueueNameCompleted),
        Level = LogLevel.Trace,
        Message = "Getting queue name for consumer {ConsumerType} completed. Queue name: {QueueName}.")]
    public static partial void GetQueueNameCompleted(
        this ILogger<NamesMappings> logger,
        Type consumerType,
        string queueName);

    private static class EventIds
    {
        public const int GetExchangeNameStarted = 1;

        public const int NoExchangeMappingFound = 2;

        public const int GetExchangeNameCompleted = 3;

        public const int GetQueueNameStarted = 4;

        public const int NoQueueMappingFound = 5;

        public const int GetQueueNameCompleted = 6;
    }
}

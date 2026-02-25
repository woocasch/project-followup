namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.Logging;

internal static partial class QueueBinderLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Queue binding of '{ConsumerType}' started.")]
    public static partial void Started(
        this ILogger<QueueBinder> logger,
        Type consumerType);

    [LoggerMessage(
        EventId = EventIds.ConnectionCreated,
        EventName = nameof(EventIds.ConnectionCreated),
        Level = LogLevel.Trace,
        Message = "Connection created for consumer '{ConsumerType}'.")]
    public static partial void ConnectionCreated(
        this ILogger<QueueBinder> logger,
        Type consumerType);

    [LoggerMessage(
        EventId = EventIds.ChannelCreated,
        EventName = nameof(EventIds.ChannelCreated),
        Level = LogLevel.Trace,
        Message = "Channel created for consumer '{ConsumerType}'.")]
    public static partial void ChannelCreated(
        this ILogger<QueueBinder> logger,
        Type consumerType);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Queue binding of '{ConsumerType}' completed.")]
    public static partial void Completed(
        this ILogger<QueueBinder> logger,
        Type consumerType);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ConnectionCreated = 2;

        public const int ChannelCreated = 3;

        public const int Completed = 4;
    }
}

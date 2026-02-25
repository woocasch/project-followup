namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.Logging;

internal static partial class ConsumerBaseLogs
{
    [LoggerMessage(
        EventId = EventIds.MessageReceived,
        EventName = nameof(EventIds.MessageReceived),
        Level = LogLevel.Trace,
        Message = "Message received")]
    public static partial void MessageReceived(
        this ILogger<IConsumerBase> logger);

    [LoggerMessage(
        EventId = EventIds.MessageDeserialized,
        EventName = nameof(EventIds.MessageDeserialized),
        Level = LogLevel.Trace,
        Message = "Message deserialized (message type '{MessageType}').")]
    public static partial void MessageDeserialized(
        this ILogger<IConsumerBase> logger,
        Type messageType);

    [LoggerMessage( 
        EventId = EventIds.MessageHandled,
        EventName = nameof(EventIds.MessageHandled),
        Level = LogLevel.Debug,
        Message = "Message handled successfully (message type '{MessageType}').")]
    public static partial void MessageHandled(
        this ILogger<IConsumerBase> logger,
        Type messageType);

    [LoggerMessage(
        EventId = EventIds.MessageHandlingError,
        EventName = nameof(EventIds.MessageHandlingError),
        Level = LogLevel.Error,
        Message = "Error handling message (message type '{MessageType}').")]
    public static partial void MessageHandlingError(
        this ILogger<IConsumerBase> logger,
        Type messageType,
        Exception exception);

    private static class EventIds
    {
        public const int MessageReceived = 1001;

        public const int MessageDeserialized = 1002;

        public const int MessageHandled = 1003;

        public const int MessageHandlingError = 1004;
    }
}

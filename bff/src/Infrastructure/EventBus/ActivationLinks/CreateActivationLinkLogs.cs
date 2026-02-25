namespace ProjectFollowUp.BFF.Infrastructure.EventBus.ActivationLinks;

using Microsoft.Extensions.Logging;

internal static partial class CreateActivationLinkLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started creating activation link for user {UserId}.")]
    public static partial void Started(
        this ILogger<CreateActivationLinkConsumer> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.SendingCommand,
        EventName = nameof(EventIds.SendingCommand),
        Level = LogLevel.Trace,
        Message = "Sending command to create activation link for user {UserId}.")]
    public static partial void SendingCommand(
        this ILogger<CreateActivationLinkConsumer> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed creating activation link for user {UserId}.")]
    public static partial void Completed(
        this ILogger<CreateActivationLinkConsumer> logger,
        Guid userId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int SendingCommand = 2;

        public const int Completed = 3;
    }
}

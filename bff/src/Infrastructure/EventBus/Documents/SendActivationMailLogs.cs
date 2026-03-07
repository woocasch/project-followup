namespace ProjectFollowUp.BFF.Infrastructure.EventBus.Documents;

using Microsoft.Extensions.Logging;

internal static partial class SendActivationMailLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started sending activation mail for activation link {ActivationLinkId}.")]
    public static partial void Started(
        this ILogger<SendActivationMailConsumer> logger,
        Guid activationLinkId);

    [LoggerMessage(
        EventId = EventIds.RetrievingLinkInformation,
        EventName = nameof(EventIds.RetrievingLinkInformation),
        Level = LogLevel.Debug,
        Message = "Retrieving information for activation link {ActivationLinkId}.")]
    public static partial void RetrievingLinkInformation(
        this ILogger<SendActivationMailConsumer> logger,
        Guid activationLinkId);

    [LoggerMessage(
        EventId = EventIds.LinkNotFound,
        EventName = nameof(EventIds.LinkNotFound),
        Level = LogLevel.Error,
        Message = "Activation link {ActivationLinkId} not found.")]
    public static partial void LinkNotFound(
        this ILogger<SendActivationMailConsumer> logger,
        Guid activationLinkId);

    [LoggerMessage(
        EventId = EventIds.SendingEmail,
        EventName = nameof(EventIds.SendingEmail),
        Level = LogLevel.Debug,
        Message = "Sending activation email for activation link {ActivationLinkId}.")]
    public static partial void SendingEmail(
        this ILogger<SendActivationMailConsumer> logger,
        Guid activationLinkId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed sending activation mail for activation link {ActivationLinkId}.")]
    public static partial void Completed(
        this ILogger<SendActivationMailConsumer> logger,
        Guid activationLinkId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int RetrievingLinkInformation = 2;

        public const int LinkNotFound = 3;

        public const int SendingEmail = 4;

        public const int Completed = 5;
    }
}

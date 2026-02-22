namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using Microsoft.Extensions.Logging;

internal static partial class CreateActivationLinkCommandLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Start creating activation link for user with id {UserId}.")]
    public static partial void Started(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.AggregateCreated,
        EventName = nameof(EventIds.AggregateCreated),
        Level = LogLevel.Trace,
        Message = "Aggregate created for user with id {UserId} and link code {LinkId}.")]
    public static partial void AggregateCreated(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.StreamStored,
        EventName = nameof(EventIds.StreamStored),
        Level = LogLevel.Trace,
        Message = "Event stream stored for user with id {UserId} and link code {LinkId}.")]
    public static partial void StreamStored(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.LinkGeneratedEventPublished,
        EventName = nameof(EventIds.LinkGeneratedEventPublished),
        Level = LogLevel.Debug,
        Message = "Link generated event published for user with id {UserId} and link code {LinkId}.")]
    public static partial void LinkGeneratedEventPublished(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed creating activation link for user with id {UserId}.")]
    public static partial void Completed(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int AggregateCreated = 2;

        public const int StreamStored = 3;

        public const int LinkGeneratedEventPublished = 4;

        public const int Completed = 5;
    }
}

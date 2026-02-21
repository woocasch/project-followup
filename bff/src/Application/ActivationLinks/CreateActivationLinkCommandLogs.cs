namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Dictionaries.Logging;

public static partial class CreateActivationLinkCommandLogs
{
    [LoggerMessage(
        EventId = ActivationLinks.CreateActivationLinkCommand_Started,
        EventName = nameof(ActivationLinks.CreateActivationLinkCommand_Started),
        Level = LogLevel.Trace,
        Message = "Start creating activation link for user with id {UserId}.")]
    public static partial void Started(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = ActivationLinks.CreateActivationLinkCommand_AggregateCreated,
        EventName = nameof(AggregateCreated),
        Level = LogLevel.Trace,
        Message = "Aggregate created for user with id {UserId} and link code {LinkId}.")]
    public static partial void AggregateCreated(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.CreateActivationLinkCommand_StreamStored,
        EventName = nameof(ActivationLinks.CreateActivationLinkCommand_StreamStored),
        Level = LogLevel.Trace,
        Message = "Event stream stored for user with id {UserId} and link code {LinkId}.")]
    public static partial void StreamStored(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.CreateActivationLinkCommand_LinkGeneratedEventPublished,
        EventName = nameof(ActivationLinks.CreateActivationLinkCommand_LinkGeneratedEventPublished),
        Level = LogLevel.Debug,
        Message = "Link generated event published for user with id {UserId} and link code {LinkId}.")]
    public static partial void LinkGeneratedEventPublished(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.CreateActivateionLinkCommand_Completed,
        EventName = nameof(ActivationLinks.CreateActivateionLinkCommand_Completed),
        Level = LogLevel.Trace,
        Message = "Completed creating activation link for user with id {UserId}.")]
    public static partial void Completed(
        this ILogger<CreateActivationLinkCommandHandler> logger,
        Guid userId);
}

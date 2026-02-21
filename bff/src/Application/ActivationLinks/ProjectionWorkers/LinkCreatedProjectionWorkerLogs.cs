namespace ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Dictionaries.Logging;

public static partial class LinkCreatedProjectionWorkerLogs
{
    [LoggerMessage(
        EventId = ActivationLinks.LinkCreatedProjectionWorker_Started,
        EventName = nameof(ActivationLinks.LinkCreatedProjectionWorker_Started),
        Level = LogLevel.Trace,
        Message = "Read model projection for link '{LinkId}' started.")]
    public static partial void Started(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.LinkCreatedProjectionWorker_UpdatingExistingLink,
        EventName = nameof(ActivationLinks.LinkCreatedProjectionWorker_UpdatingExistingLink),
        Level = LogLevel.Debug,
        Message = "Updating existing activation link with id '{LinkId}'.")]
    public static partial void UpdatingExistingLink(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.LinkCreatedProjectionWorker_CreatingNewLink,
        EventName = nameof(ActivationLinks.LinkCreatedProjectionWorker_CreatingNewLink),
        Level = LogLevel.Debug,
        Message = "Creating new activation link with id '{LinkId}'.")]
    public static partial void CreatingNewLink(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = ActivationLinks.LinkCreatedProjectionWorker_Completed,
        EventName = nameof(ActivationLinks.LinkCreatedProjectionWorker_Completed),
        Level = LogLevel.Trace,
        Message = "Read model projection for link '{LinkId}' completed.")]
    public static partial void Completed(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);
}

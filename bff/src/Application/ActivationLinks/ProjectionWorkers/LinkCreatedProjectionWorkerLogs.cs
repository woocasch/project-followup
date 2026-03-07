namespace ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class LinkCreatedProjectionWorkerLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Read model projection for link '{LinkId}' started.")]
    public static partial void Started(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.UpdatingExistingLink,
        EventName = nameof(EventIds.UpdatingExistingLink),
        Level = LogLevel.Debug,
        Message = "Updating existing activation link with id '{LinkId}'.")]
    public static partial void UpdatingExistingLink(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.CreatingNewLink,
        EventName = nameof(EventIds.CreatingNewLink),
        Level = LogLevel.Debug,
        Message = "Creating new activation link with id '{LinkId}'.")]
    public static partial void CreatingNewLink(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Read model projection for link '{LinkId}' completed.")]
    public static partial void Completed(
        this ILogger<LinkCreatedProjectionWorker> logger,
        Guid linkId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int UpdatingExistingLink = 2;

        public const int CreatingNewLink = 3;

        public const int Completed = 4;
    }
}

namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class FetchProjectsQueryLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing FetchProjectsQuery for UserId: {UserId}.")]
    public static partial void Started(
        this ILogger<FetchProjectsQueryHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing FetchProjectsQuery for UserId: {UserId}.")]
    public static partial void Completed(
        this ILogger<FetchProjectsQueryHandler> logger,
        Guid userId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int Completed = 2;
    }
}

namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class FetchProjectTasksQueryLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing FetchProjectTasksQuery for project: {ProjectId}.")]
    public static partial void Started(
        this ILogger<FetchProjectTasksQueryHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing FetchProjectTasksQuery for project: {ProjectId}.")]
    public static partial void Completed(
        this ILogger<FetchProjectTasksQueryHandler> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int Completed = 2;
    }
}

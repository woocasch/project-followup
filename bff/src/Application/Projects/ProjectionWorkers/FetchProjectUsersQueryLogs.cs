namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class FetchProjectUsersQueryLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Starting retrieval of project users for project '{ProjectId}'")]
    public static partial void Started(
        this ILogger<FetchProjectUsersQueryHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectNotFound,
        EventName = nameof(EventIds.ProjectNotFound),
        Level = LogLevel.Warning,
        Message = "Project with id '{ProjectId}' was not found")]
    public static partial void ProjectNotFound(
        this ILogger<FetchProjectUsersQueryHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed retrieval of project users for project '{ProjectId}'")]
    public static partial void Completed(
        this ILogger<FetchProjectUsersQueryHandler> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectNotFound = 2;

        public const int Completed = 3;
    }
}

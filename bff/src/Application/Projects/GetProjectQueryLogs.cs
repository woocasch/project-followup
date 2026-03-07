namespace ProjectFollowUp.BFF.Application.Projects;

using Microsoft.Extensions.Logging;

internal static partial class GetProjectQueryLogs
{
    [LoggerMessage(
        EventId  = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Starting retrieval of project {ProjectId} and user {UserId}")]
    public static partial void Started(
        this ILogger<GetProjectQueryHandler> logger,
        Guid projectId,
        Guid userId);

    [LoggerMessage(
        EventId  = EventIds.ProjectNotFound,
        EventName = nameof(EventIds.ProjectNotFound),
        Level = LogLevel.Warning,
        Message = "Project {ProjectId} was not found")]
    public static partial void ProjectNotFound(
        this ILogger<GetProjectQueryHandler> logger,
        Guid projectId);

    [LoggerMessage(
        EventId  = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed retrieval of project {ProjectId}")]
    public static partial void Completed(
        this ILogger<GetProjectQueryHandler> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectNotFound = 2;

        public const int Completed = 3;
    }
}

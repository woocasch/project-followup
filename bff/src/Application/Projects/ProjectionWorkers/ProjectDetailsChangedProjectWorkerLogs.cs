namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class ProjectDetailsChangedProjectWorkerLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing project details changed event for project with id {ProjectId}.")]
    public static partial void Started(
        this ILogger<ProjectDetailsChangedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectNotFound,
        EventName = nameof(EventIds.ProjectNotFound),
        Level = LogLevel.Warning,
        Message = "Project with id {ProjectId} was not found.")]
    public static partial void ProjectNotFound(
        this ILogger<ProjectDetailsChangedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.UpdatingProject,
        EventName = nameof(EventIds.UpdatingProject),
        Level = LogLevel.Debug,
        Message = "Updating project with id {ProjectId}.")]
    public static partial void UpdatingProject(
        this ILogger<ProjectDetailsChangedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing project details changed event for project with id {ProjectId}.")]
    public static partial void Completed(
        this ILogger<ProjectDetailsChangedProjectionWorker> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectNotFound = 2;

        public const int UpdatingProject = 3;

        public const int Completed = 4;
    }
}

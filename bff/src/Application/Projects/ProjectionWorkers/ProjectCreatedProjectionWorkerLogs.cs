namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class ProjectCreatedProjectionWorkerLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Starting materialization of '{ProjectCreatedEventType}', project id: '{ProjectId}'")]
    public static partial void Started(
        this ILogger<ProjectCreatedProjectionWorker> logger,
        Type projectCreatedEventType,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectAlreadyExists,
        EventName = nameof(EventIds.ProjectAlreadyExists),
        Level = LogLevel.Debug,
        Message = "Project with id '{ProjectId}' already exists in read model.")]
    public static partial void ProjectAlreadyExists(
        this ILogger<ProjectCreatedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectUpdated,
        EventName = nameof(EventIds.ProjectUpdated),
        Level = LogLevel.Trace,
        Message = "Project with id '{ProjectId}' updated in read model to creation state.")]
    public static partial void ProjectUpdated(
        this ILogger<ProjectCreatedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectNotExists,
        EventName = nameof(EventIds.ProjectNotExists),
        Level = LogLevel.Debug,
        Message = "Project with id '{ProjectId}' does not exist in read model.")]
    public static partial void ProjectNotExists(
        this ILogger<ProjectCreatedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.ProjectInserted,
        EventName = nameof(EventIds.ProjectInserted),
        Level = LogLevel.Trace,
        Message = "Project with id '{ProjectId}' inserted in read model.")]
    public static partial void ProjectInserted(
        this ILogger<ProjectCreatedProjectionWorker> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectAlreadyExists = 2;

        public const int ProjectUpdated = 3;

        public const int ProjectNotExists = 4;

        public const int ProjectInserted = 5;
    }
}

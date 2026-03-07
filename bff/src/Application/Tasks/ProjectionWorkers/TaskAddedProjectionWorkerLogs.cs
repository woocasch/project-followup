namespace ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class TaskAddedProjectionWorkerLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Starting new task materialization for project {ProjectId}, task {TaskId}.")]
    public static partial void Started(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.UpdatingExistingProjection,
        EventName = nameof(EventIds.UpdatingExistingProjection),
        Level = LogLevel.Trace,
        Message = "Updating existing task projection for project {ProjectId}, task {TaskId}.")]
    public static partial void UpdatingExistingProjection(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.ProjectionUpdated,
        EventName = nameof(EventIds.ProjectionUpdated),
        Level = LogLevel.Trace,
        Message = "Existing task projection updated for project {ProjectId}, task {TaskId}.")]
    public static partial void ProjectionUpdated(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.CreatingNewProjection,
        EventName = nameof(EventIds.CreatingNewProjection),
        Level = LogLevel.Trace,
        Message = "Creating new task projection for project {ProjectId}, task {TaskId}.")]
    public static partial void CreatingNewProjection(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.ProjectionCreated,
        EventName = nameof(EventIds.ProjectionCreated),
        Level = LogLevel.Debug,
        Message = "New task projection created for project {ProjectId}, task {TaskId}.")]
    public static partial void ProjectionCreated(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int UpdatingExistingProjection = 2;

        public const int ProjectionUpdated = 3;

        public const int CreatingNewProjection = 4;

        public const int ProjectionCreated = 5;
    }
}

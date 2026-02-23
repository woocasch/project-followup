namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Domain.Project;

internal static partial class TaskStatusChangedProjectionWorkerBaseLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing task status change for TaskId: {TaskId} in ProjectId: {ProjectId}, target status: {Status}.")]
    public static partial void Started(
        this ILogger<ITaskStatusChangedProjectionWorker> logger,
        Guid projectId,
        Guid taskId,
        ProjectTaskStatus status);

    [LoggerMessage(
        EventId = EventIds.ProjectNotFound,
        EventName = nameof(EventIds.ProjectNotFound),
        Level = LogLevel.Error,
        Message = "Project with id {ProjectId} was not found.")]
    public static partial void ProjectNotFound(
        this ILogger<ITaskStatusChangedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.TaskNotFound,
        EventName = nameof(EventIds.TaskNotFound),
        Level = LogLevel.Error,
        Message = "Task with id {TaskId} was not found in project with id {ProjectId}.")]
    public static partial void TaskNotFound(
        this ILogger<ITaskStatusChangedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.SendingUpdate,
        EventName = nameof(EventIds.SendingUpdate),
        Level = LogLevel.Debug,
        Message = "Sending update for project with id {ProjectId}.")]
    public static partial void SendingUpdate(
        this ILogger<ITaskStatusChangedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing task status change for TaskId: {TaskId} in ProjectId: {ProjectId}.")]
    public static partial void Completed(
        this ILogger<ITaskStatusChangedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectNotFound = 2;

        public const int TaskNotFound = 3;

        public const int SendingUpdate = 4;

        public const int Completed = 5;
    }
}

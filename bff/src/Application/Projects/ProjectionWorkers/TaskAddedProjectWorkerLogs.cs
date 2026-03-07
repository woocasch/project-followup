namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class TaskAddedProjectWorkerLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started processing task added event for project with id '{ProjectId}' and task with id '{TaskId}'.")]
    public static partial void Started(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.ProjectNotFound,
        EventName = nameof(EventIds.ProjectNotFound),
        Level = LogLevel.Error,
        Message = "Project with id '{ProjectId}' was not found.")]
    public static partial void ProjectNotFound(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.TaskAdded,
        EventName = nameof(EventIds.TaskAdded),
        Level = LogLevel.Trace,
        Message = "Task with id '{TaskId}' was added to project with id '{ProjectId}'.")]
    public static partial void TaskAdded(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.TaskUpdated,
        EventName = nameof(EventIds.TaskUpdated),
        Level = LogLevel.Trace,
        Message = "Task with id '{TaskId}' was updated in project with id '{ProjectId}'.")]
    public static partial void TaskUpdated(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.SavingUpdatedProject,
        EventName = nameof(EventIds.SavingUpdatedProject),
        Level = LogLevel.Debug,
        Message = "Saving updated project with id '{ProjectId}'.")]
    public static partial void SavingUpdatedProject(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed processing task added event for project with id '{ProjectId}' and task with id '{TaskId}'.")]
    public static partial void Completed(
        this ILogger<TaskAddedProjectionWorker> logger,
        Guid projectId,
        Guid taskId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectNotFound = 2;

        public const int TaskAdded = 3;

        public const int TaskUpdated = 4;

        public const int SavingUpdatedProject = 5;

        public const int Completed = 6;
    }
}

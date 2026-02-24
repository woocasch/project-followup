namespace ProjectFollowUp.BFF.Application.Tasks;

using Microsoft.Extensions.Logging;

internal static partial class CreateTaskCommandLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Starting task creation for project {ProjectId}, task {TaskId}.")]
    public static partial void Started(
        this ILogger<CreateTaskCommandHandler> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.ProjectNotFound,
        EventName = nameof(EventIds.ProjectNotFound),
        Level = LogLevel.Error,
        Message = "Project with ID {ProjectId} not found when trying to create task {TaskId}.")]
    public static partial void ProjectNotFound(
        this ILogger<CreateTaskCommandHandler> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.ProjectRehydrated,
        EventName = nameof(EventIds.ProjectRehydrated),
        Level = LogLevel.Trace,
        Message = "Project with ID {ProjectId} rehydrated when creating task {TaskId}.")]
    public static partial void ProjectRehydrated(
        this ILogger<CreateTaskCommandHandler> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.TaskAdded,
        EventName = nameof(EventIds.TaskAdded),
        Level = LogLevel.Debug,
        Message = "Task {TaskId} added to project {ProjectId}.")]
    public static partial void TaskAdded(
        this ILogger<CreateTaskCommandHandler> logger,
        Guid projectId,
        Guid taskId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed task creation for project {ProjectId}, task {TaskId}.")]
    public static partial void Completed(
        this ILogger<CreateTaskCommandHandler> logger,
        Guid projectId,
        Guid taskId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int ProjectNotFound = 2;

        public const int ProjectRehydrated = 3;

        public const int TaskAdded = 4;

        public const int Completed = 5;
    }
}

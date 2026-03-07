namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.Extensions.Logging;

internal static partial class TasksControllerLogs
{
    [LoggerMessage(
        EventId = EventIds.FetchListStarted,
        EventName = nameof(EventIds.FetchListStarted),
        Level = LogLevel.Trace,
        Message = "Fetching tasks for project '{ProjectId}'")]
    public static partial void FetchListStarted(
        this ILogger<TasksController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.FetchListQueryExecuted,
        EventName = nameof(EventIds.FetchListQueryExecuted),
        Level = LogLevel.Debug,
        Message = "Fetch tasks query executed for project '{ProjectId}'")]
    public static partial void FetchListQueryExecuted(
        this ILogger<TasksController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.FetchListResultMapped,
        EventName = nameof(EventIds.FetchListResultMapped),
        Level = LogLevel.Trace,
        Message = "Fetch tasks result mapped for project '{ProjectId}' with {TaskCount} tasks")]
    public static partial void FetchListResultMapped(
        this ILogger<TasksController> logger,
        Guid projectId,
        int taskCount);

    [LoggerMessage(
        EventId = EventIds.CreateTaskStarted,
        EventName = nameof(EventIds.CreateTaskStarted),
        Level = LogLevel.Trace,
        Message = "Creating task for project '{ProjectId}' with title '{Title}'")]
    public static partial void CreateTaskStarted(
        this ILogger<TasksController> logger,
        Guid projectId,
        string title);

    [LoggerMessage(
        EventId = EventIds.CreateTaskInputProcessed,
        EventName = nameof(EventIds.CreateTaskInputProcessed),
        Level = LogLevel.Debug,
        Message = "Create task input processed for project '{ProjectId}' with title '{Title}'")]
    public static partial void CreateTaskInputProcessed(
        this ILogger<TasksController> logger,
        Guid projectId,
        string title);

    [LoggerMessage(
        EventId = EventIds.CreateTaskCommandFailed,
        EventName = nameof(EventIds.CreateTaskCommandFailed),
        Level = LogLevel.Error,
        Message = "Failed to create task for project '{ProjectId}' with title '{Title}': {ErrorCode}")]
    public static partial void CreateTaskCommandFailed(
        this ILogger<TasksController> logger,
        Guid projectId,
        string title,
        string errorCode,
        Exception? exception);

    [LoggerMessage(
        EventId = EventIds.CreateTaskCommandSucceeded,
        EventName = nameof(EventIds.CreateTaskCommandSucceeded),
        Level = LogLevel.Debug,
        Message = "Successfully created task for project '{ProjectId}' with title '{Title}'")]
    public static partial void CreateTaskCommandSucceeded(
        this ILogger<TasksController> logger,
        Guid projectId,
        string title);

    [LoggerMessage(
        EventId = EventIds.CreateCommandCompleted,
        EventName = nameof(EventIds.CreateCommandCompleted),
        Level = LogLevel.Trace,
        Message = "Create task command completed for project '{ProjectId}' with title '{Title}'")]
    public static partial void CreateCommandCompleted(
        this ILogger<TasksController> logger,
        Guid projectId,
        string title);

    private static class EventIds
    {
        public const int FetchListStarted = 1;

        public const int FetchListQueryExecuted = 2;

        public const int FetchListResultMapped = 3;

        public const int CreateTaskStarted = 4;

        public const int CreateTaskInputProcessed = 5;

        public const int CreateTaskCommandFailed = 6;

        public const int CreateTaskCommandSucceeded = 7;

        public const int CreateCommandCompleted = 8;
    }
}

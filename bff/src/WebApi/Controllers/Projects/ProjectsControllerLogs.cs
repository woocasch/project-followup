namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;

internal static partial class ProjectsControllerLogs
{
    [LoggerMessage(
        EventId = EventIds.FetchStarted,
        EventName = nameof(EventIds.FetchStarted),
        Level = LogLevel.Trace,
        Message = "Started fetching projects")]
    public static partial void FetchListStarted(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.FetchUserNotAuthorized,
        EventName = nameof(EventIds.FetchUserNotAuthorized),
        Level = LogLevel.Error,
        Message = "User is not authorized to fetch projects")]
    public static partial void FetchListUserNotAuthorized(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.FetchFetchingData,
        EventName = nameof(EventIds.FetchFetchingData),
        Level = LogLevel.Trace,
        Message = "Fetching projects data")]
    public static partial void FetchListFetchingData(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.FetchCompleted,
        EventName = nameof(EventIds.FetchCompleted),
        Level = LogLevel.Trace,
        Message = "Completed fetching projects")]
    public static partial void FetchListCompleted(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.GetStarted,
        EventName = nameof(EventIds.GetStarted),
        Level = LogLevel.Trace,
        Message = "Started fetching project details")]
    public static partial void GetStarted(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.GetUserNotAuthorized,
        EventName = nameof(EventIds.GetUserNotAuthorized),
        Level = LogLevel.Error,
        Message = "User is not authorized to fetch project details")]
    public static partial void GetUserNotAuthorized(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.GetFetchingData,
        EventName = nameof(EventIds.GetFetchingData),
        Level = LogLevel.Trace,
        Message = "Fetching project details data")]
    public static partial void GetFetchingData(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.GetProjectNotFound,
        EventName = nameof(EventIds.GetProjectNotFound),
        Level = LogLevel.Warning,
        Message = "Project with id {ProjectId} was not found")]
    public static partial void GetProjectNotFound(
        this ILogger<ProjectsController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.GetCompleted,
        EventName = nameof(EventIds.GetCompleted),
        Level = LogLevel.Trace,
        Message = "Completed fetching project details")]
    public static partial void GetCompleted(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.CreateStarted,
        EventName = nameof(EventIds.CreateStarted),
        Level = LogLevel.Trace,
        Message = "Started creating project")]
    public static partial void CreateStarted(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.CreateUserNotAuthorized,
        EventName = nameof(EventIds.CreateUserNotAuthorized),
        Level = LogLevel.Error,
        Message = "User is not authorized to create project")]
    public static partial void CreateUserNotAuthorized(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.CreateCreatingProject,
        EventName = nameof(EventIds.CreateCreatingProject),
        Level = LogLevel.Trace,
        Message = "Creating project")]
    public static partial void CreateCreatingProject(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.CreateCreationFailed,
        EventName = nameof(EventIds.CreateCreationFailed),
        Level = LogLevel.Error,
        Message = "Failed to create project")]
    public static partial void CreateCreationFailed(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.CreateCompleted,
        EventName = nameof(EventIds.CreateCompleted),
        Level = LogLevel.Trace,
        Message = "Completed creating project with id {ProjectId}")]
    public static partial void CreateCompleted(
        this ILogger<ProjectsController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.UpdateStarted,
        EventName = nameof(EventIds.UpdateStarted),
        Level = LogLevel.Trace,
        Message = "Started updating project")]
    public static partial void UpdateStarted(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.UpdateUserNotAuthorized,
        EventName = nameof(EventIds.UpdateUserNotAuthorized),
        Level = LogLevel.Error,
        Message = "User is not authorized to update project")]
    public static partial void UpdateUserNotAuthorized(
        this ILogger<ProjectsController> logger);

    [LoggerMessage(
        EventId = EventIds.UpdateUpdatingProject,
        EventName = nameof(EventIds.UpdateUpdatingProject),
        Level = LogLevel.Trace,
        Message = "Updating project with id {ProjectId}")]
    public static partial void UpdateUpdatingProject(
        this ILogger<ProjectsController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.UpdateUpdateFailed,
        EventName = nameof(EventIds.UpdateUpdateFailed),
        Level = LogLevel.Error,
        Message = "Failed to update project with id {ProjectId}")]
    public static partial void UpdateUpdateFailed(
        this ILogger<ProjectsController> logger,
        Guid projectId);

    [LoggerMessage(
        EventId = EventIds.UpdateCompleted,
        EventName = nameof(EventIds.UpdateCompleted),
        Level = LogLevel.Trace,
        Message = "Completed updating project with id {ProjectId}")]
    public static partial void UpdateCompleted(
        this ILogger<ProjectsController> logger,
        Guid projectId);

    private static class EventIds
    {
        public const int FetchStarted = 1;

        public const int FetchUserNotAuthorized = 2;

        public const int FetchFetchingData = 3;

        public const int FetchCompleted = 4;

        public const int GetStarted = 5;

        public const int GetUserNotAuthorized = 6;

        public const int GetFetchingData = 7;

        public const int GetProjectNotFound = 8;

        public const int GetCompleted = 9;

        public const int CreateStarted = 10;

        public const int CreateUserNotAuthorized = 11;

        public const int CreateCreatingProject = 12;

        public const int CreateCreationFailed = 13;

        public const int CreateCompleted = 14;

        public const int UpdateStarted = 15;

        public const int UpdateUserNotAuthorized = 16;

        public const int UpdateUpdatingProject = 17;

        public const int UpdateUpdateFailed = 18;

        public const int UpdateCompleted = 19;
    }
}

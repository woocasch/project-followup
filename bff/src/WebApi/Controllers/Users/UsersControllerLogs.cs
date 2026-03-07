namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

internal static partial class UsersControllerLogs
{
    [LoggerMessage(
        EventId = EventIds.CreateStarted,
        EventName = nameof(EventIds.CreateStarted),
        Level = LogLevel.Trace,
        Message = "Started creating user '{EmailAddress}'.")]
    public static partial void CreateStarted(
        this ILogger<UsersController> logger,
        string emailAddress);

    [LoggerMessage(
        EventId = EventIds.CreateCommandExecuted,
        EventName = nameof(EventIds.CreateCommandExecuted),
        Level = LogLevel.Debug,
        Message = "Create user command executed for '{EmailAddress}'.")]
    public static partial void CreateCommandExecuted(
        this ILogger<UsersController> logger,
        string emailAddress);

    [LoggerMessage(
        EventId = EventIds.CreateCommandFailed,
        EventName = nameof(EventIds.CreateCommandFailed),
        Level = LogLevel.Error,
        Message = "Create user command failed for '{EmailAddress}' with error code '{ErrorCode}'.")]
    public static partial void CreateCommandFailed(
        this ILogger<UsersController> logger,
        string emailAddress,
        string errorCode,
        Exception? ex);

    [LoggerMessage(
        EventId = EventIds.CreateCompleted,
        EventName = nameof(EventIds.CreateCompleted),
        Level = LogLevel.Trace,
        Message = "Completed creating user '{EmailAddress}'.")]
    public static partial void CreateCompleted(
        this ILogger<UsersController> logger,
        string emailAddress);

    private static class EventIds
    {
        public const int CreateStarted = 1;

        public const int CreateCommandExecuted = 2;

        public const int CreateCommandFailed = 3;

        public const int CreateCompleted = 4;
    }
}

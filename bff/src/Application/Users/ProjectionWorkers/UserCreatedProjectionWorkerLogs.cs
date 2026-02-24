namespace ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

using Microsoft.Extensions.Logging;

internal static partial class UserCreatedProjectionWorkerLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started projection worker for creation of user {UserId}.")]
    public static partial void Started(
        this ILogger<UserCreatedProjectionWorker> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.UserExists,
        EventName = nameof(EventIds.UserExists),
        Level = LogLevel.Trace,
        Message = "User with id {UserId} exists.")]
    public static partial void UserExists(
        this ILogger<UserCreatedProjectionWorker> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.UserUpdated,
        EventName = nameof(EventIds.UserUpdated),
        Level = LogLevel.Debug,
        Message = "User with id {UserId} updated.")]
    public static partial void UserUpdated(
        this ILogger<UserCreatedProjectionWorker> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.UserNotExists,
        EventName = nameof(EventIds.UserNotExists),
        Level = LogLevel.Trace,
        Message = "User with id {UserId} does not exist.")]
    public static partial void UserNotExists(
        this ILogger<UserCreatedProjectionWorker> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.UserCreated,
        EventName = nameof(EventIds.UserCreated),
        Level = LogLevel.Debug,
        Message = "User with id {UserId} created.")]
    public static partial void UserCreated(
        this ILogger<UserCreatedProjectionWorker> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed projection worker for creation of user {UserId}.")]
    public static partial void Completed(
        this ILogger<UserCreatedProjectionWorker> logger,
        Guid userId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int UserExists = 2;

        public const int UserUpdated = 3;

        public const int UserNotExists = 4;

        public const int UserCreated = 5;

        public const int Completed = 6;
    }
}

namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

internal static partial class UsersReadModelLogs
{
    [LoggerMessage(
        EventId = EventIds.GetByIdStarted,
        EventName = nameof(EventIds.GetByIdStarted),
        Level = LogLevel.Trace,
        Message = "Getting user '{UserId}' started.")]
    public static partial void GetByIdStarted(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetByIdItemsRetrieved,
        EventName = nameof(EventIds.GetByIdItemsRetrieved),
        Level = LogLevel.Debug,
        Message = "Getting user '{UserId}' retrieved items.")]
    public static partial void GetByIdItemsRetrieved(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetByIdUserNotFound,
        EventName = nameof(EventIds.GetByIdUserNotFound),
        Level = LogLevel.Warning,
        Message = "Getting user '{UserId}' not found.")]
    public static partial void GetByIdUserNotFound(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetByIdCompleted,
        EventName = nameof(EventIds.GetByIdCompleted),
        Level = LogLevel.Trace,
        Message = "Getting user '{UserId}' completed.")]
    public static partial void GetByIdCompleted(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetByEmailStarted,
        EventName = nameof(EventIds.GetByEmailStarted),
        Level = LogLevel.Trace,
        Message = "Getting user '{Email}' started.")]
    public static partial void GetByEmailStarted(
        this ILogger<UsersReadModel> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.GetByEmailItemsRetrieved,
        EventName = nameof(EventIds.GetByEmailItemsRetrieved),
        Level = LogLevel.Debug,
        Message = "Getting user '{Email}' retrieved items.")]
    public static partial void GetByEmailItemsRetrieved(
        this ILogger<UsersReadModel> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.GetByEmailUserNotFound,
        EventName = nameof(EventIds.GetByEmailUserNotFound),
        Level = LogLevel.Warning,
        Message = "Getting user '{Email}' not found.")]
    public static partial void GetByEmailUserNotFound(
        this ILogger<UsersReadModel> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.GetByEmailCompleted,
        EventName = nameof(EventIds.GetByEmailCompleted),
        Level = LogLevel.Trace,
        Message = "Getting user '{Email}' completed.")]
    public static partial void GetByEmailCompleted(
        this ILogger<UsersReadModel> logger,
        string email);

    private static class EventIds
    {
        public const int GetByIdStarted = 1;

        public const int GetByIdItemsRetrieved = 2;

        public const int GetByIdUserNotFound = 3;

        public const int GetByIdCompleted = 4;

        public const int GetByEmailStarted = 5;

        public const int GetByEmailItemsRetrieved = 6;

        public const int GetByEmailUserNotFound = 7;

        public const int GetByEmailCompleted = 8;
    }
}

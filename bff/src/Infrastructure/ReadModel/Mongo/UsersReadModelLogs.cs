namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

internal static partial class UsersReadModelLogs
{
    [LoggerMessage(
        EventId = EventIds.GetStarted,
        EventName = nameof(EventIds.GetStarted),
        Level = LogLevel.Trace,
        Message = "Getting user '{UserId}' started.")]
    public static partial void GetStarted(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetItemsRetrieved,
        EventName = nameof(EventIds.GetItemsRetrieved),
        Level = LogLevel.Debug,
        Message = "Getting user '{UserId}' retrieved items.")]
    public static partial void GetItemsRetrieved(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetUserNotFound,
        EventName = nameof(EventIds.GetUserNotFound),
        Level = LogLevel.Warning,
        Message = "Getting user '{UserId}' not found.")]
    public static partial void GetUserNotFound(
        this ILogger<UsersReadModel> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.GetCompleted,
        EventName = nameof(EventIds.GetCompleted),
        Level = LogLevel.Trace,
        Message = "Getting user '{UserId}' completed.")]
    public static partial void GetCompleted(
        this ILogger<UsersReadModel> logger,
        Guid userId);


    private static class EventIds
    {
        public const int GetStarted = 1;

        public const int GetItemsRetrieved = 2;

        public const int GetUserNotFound = 3;

        public const int GetCompleted = 4;
    }
}

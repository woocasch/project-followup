namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

internal static partial class ActivationLinksReadModelLogs
{
    [LoggerMessage(
        EventId = EventIds.GetStarted,
        EventName = nameof(EventIds.GetStarted),
        Level = LogLevel.Trace,
        Message = "Started get link by link code '{LinkCode}'")]
    public static partial void GetStarted(
        this ILogger<ActivationLinksReadModel> logger,
        string linkCode);

    [LoggerMessage(
        EventId = EventIds.GetRetrieveData,
        EventName = nameof(EventIds.GetRetrieveData),
        Level = LogLevel.Trace,
        Message = "Retrieving data from database for link code '{LinkCode}'")]
    public static partial void GetRetrieveData(
        this ILogger<ActivationLinksReadModel> logger,
        string linkCode);

    [LoggerMessage(
        EventId = EventIds.GetNoItemsFound,
        EventName = nameof(EventIds.GetNoItemsFound),
        Level = LogLevel.Warning,
        Message = "No items found for link code '{LinkCode}'")]
    public static partial void GetNoItemsFound(
        this ILogger<ActivationLinksReadModel> logger,
        string linkCode);

    [LoggerMessage(
        EventId = EventIds.GetCompleted,
        EventName = nameof(EventIds.GetCompleted),
        Level = LogLevel.Trace,
        Message = "Completed get link by link code '{LinkCode}'")]
    public static partial void GetCompleted(
        this ILogger<ActivationLinksReadModel> logger,
        string linkCode);

    private static class EventIds
    {
        public const int GetStarted = 1;

        public const int GetRetrieveData = 2;

        public const int GetNoItemsFound = 3;

        public const int GetCompleted = 4;
    }
}

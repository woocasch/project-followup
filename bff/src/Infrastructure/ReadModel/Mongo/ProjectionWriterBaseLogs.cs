namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

internal static partial class ProjectionWriterBaseLogs
{
    [LoggerMessage(
        EventId = EventIds.GetStarted,
        EventName = nameof(EventIds.GetStarted),
        Level = LogLevel.Trace, 
        Message = "Getting projection with started.")]
    public static partial void GetStarted(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.GetSendingQuery,
        EventName = nameof(EventIds.GetSendingQuery),
        Level = LogLevel.Debug, 
        Message = "Sending query to get projection.")]
    public static partial void GetSendingQuery(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.GetNoItemsFound,
        EventName = nameof(EventIds.GetNoItemsFound),
        Level = LogLevel.Warning,
        Message = "No items found for projection.")]
    public static partial void GetNoItemsFound(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.GetCompleted,
        EventName = nameof(EventIds.GetCompleted),
        Level = LogLevel.Trace, 
        Message = "Getting projection completed.")]
    public static partial void GetCompleted(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.InsertStarted,
        EventName = nameof(EventIds.InsertStarted),
        Level = LogLevel.Trace, 
        Message = "Inserting projection started.")]
    public static partial void InsertStarted(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.InsertCompleted,
        EventName = nameof(EventIds.InsertCompleted),
        Level = LogLevel.Trace, 
        Message = "Inserting projection completed.")]
    public static partial void InsertCompleted(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.UpdateStarted,
        EventName = nameof(EventIds.UpdateStarted),
        Level = LogLevel.Trace, 
        Message = "Updating projection started.")]
    public static partial void UpdateStarted(
        this ILogger<IProjectionWriterBase> logger);

    [LoggerMessage(
        EventId = EventIds.UpdateCompleted,
        EventName = nameof(EventIds.UpdateCompleted),
        Level = LogLevel.Trace, 
        Message = "Updating projection completed.")]
    public static partial void UpdateCompleted(
        this ILogger<IProjectionWriterBase> logger);

    private static class EventIds
    {
        public const int GetStarted = 1001;

        public const int GetSendingQuery = 1002;

        public const int GetNoItemsFound = 1003;

        public const int GetCompleted = 1004;

        public const int InsertStarted = 1005;

        public const int InsertCompleted = 1006;

        public const int UpdateStarted = 1007;

        public const int UpdateCompleted = 1008;
    }
}

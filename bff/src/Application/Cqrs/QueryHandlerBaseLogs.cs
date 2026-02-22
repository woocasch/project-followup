namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

internal static partial class QueryHandlerBaseLogs
{
    [LoggerMessage(
        EventId = EventIds.HandleStarted,
        EventName = nameof(EventIds.HandleStarted),
        Level = LogLevel.Trace,
        Message = "Handling query of type {QueryType}.")]
    public static partial void HandleStarted(
        this ILogger<IQueryHandlerBase> logger,
        Type queryType);

    [LoggerMessage(
        EventId = EventIds.InvalidQueryType,
        EventName = nameof(EventIds.InvalidQueryType),
        Level = LogLevel.Error,
        Message = "Invalid query type '{ReceivedQueryType}', expected '{ExpectedQueryType}'.")]
    public static partial void InvalidQueryType(
        this ILogger<IQueryHandlerBase> logger,
        Type expectedQueryType,
        Type receivedQueryType);

    [LoggerMessage(
        EventId = EventIds.PassingToStronglyTypedHandle,
        EventName = nameof(EventIds.PassingToStronglyTypedHandle),
        Level = LogLevel.Debug,
        Message = "Passing query '{QueryType}' to strongly typed Handle method.")]
    public static partial void PassingToStronglyTypedHandle(
        this ILogger<IQueryHandlerBase> logger,
        Type queryType);

    [LoggerMessage(
        EventId = EventIds.ExceptionOccurred,
        EventName = nameof(EventIds.ExceptionOccurred),
        Level = LogLevel.Error,
        Message = "An exception occurred while handling query of type {QueryType}.")]
    public static partial void ExceptionOccurred(
        this ILogger<IQueryHandlerBase> logger,
        Type queryType,
        Exception exception);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed handling query of type {QueryType}.")]
    public static partial void Completed(
        this ILogger<IQueryHandlerBase> logger,
        Type queryType);

    private static class EventIds
    {
        public const int HandleStarted = 1001;

        public const int InvalidQueryType = 1002;

        public const int PassingToStronglyTypedHandle = 1003;

        public const int ExceptionOccurred = 1004;

        public const int Completed = 1005;
    }
}

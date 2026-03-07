namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

internal static partial class MediatorLogs
{
    [LoggerMessage(
        EventId = EventIds.SendStarted,
        EventName = nameof(EventIds.SendStarted),
        Level = LogLevel.Trace,
        Message = "Started processing command '{CommandType}'.")]
    public static partial void SendStarted(
        this ILogger<Mediator> logger,
        Type commandType);

    [LoggerMessage(
        EventId = EventIds.CommandHandlerNotFound,
        EventName = nameof(EventIds.CommandHandlerNotFound),
        Level = LogLevel.Error,
        Message = "No handler found for command of type {CommandType}.")]
    public static partial void CommandHandlerNotFound(
        this ILogger<Mediator> logger,
        Type commandType);

    [LoggerMessage(
        EventId = EventIds.CallingCommandHandler,
        EventName = nameof(EventIds.CallingCommandHandler),
        Level = LogLevel.Debug,
        Message = "Calling handler of type {HandlerType} for command of type {CommandType}.")]
    public static partial void CallingCommandHandler(
        this ILogger<Mediator> logger,
        Type handlerType,
        Type commandType);

    [LoggerMessage(
        EventId = EventIds.CommandHandlerThrownException,
        EventName = nameof(EventIds.CommandHandlerThrownException),
        Level = LogLevel.Error,
        Message = "Command handler of type {HandlerType} threw an exception while handling command of type {CommandType}.")]
    public static partial void CommandHandlerThrownException(
        this ILogger<Mediator> logger,
        Type handlerType,
        Type commandType,
        Exception exception);

    [LoggerMessage(
        EventId = EventIds.CommandHandlerReturnedResult,
        EventName = nameof(EventIds.CommandHandlerReturnedResult),
        Level = LogLevel.Trace,
        Message = "Command handler of type {HandlerType} returned result '{IsSuccess}' for command of type {CommandType}.")]
    public static partial void CommandHandlerReturnedResult(
        this ILogger<Mediator> logger,
        Type handlerType,
        Type commandType,
        bool isSuccess);

    [LoggerMessage(
        EventId = EventIds.FetchStarted,
        EventName = nameof(EventIds.FetchStarted),
        Level = LogLevel.Trace,
        Message = "Fetching result for query of type {QueryType}.")]
    public static partial void FetchStarted(
        this ILogger<Mediator> logger,
        Type queryType);

    [LoggerMessage(
        EventId = EventIds.QueryHandlerNotFound,
        EventName = nameof(EventIds.QueryHandlerNotFound),
        Level = LogLevel.Error,
        Message = "No handler found for query of type {QueryType}.")]
    public static partial void QueryHandlerNotFound(
        this ILogger<Mediator> logger,
        Type queryType);

    [LoggerMessage(
        EventId = EventIds.CallingQueryHandler,
        EventName = nameof(EventIds.CallingQueryHandler),
        Level = LogLevel.Debug,
        Message = "Calling handler of type {HandlerType} for query of type {QueryType}.")]
    public static partial void CallingQueryHandler(
        this ILogger<Mediator> logger,
        Type handlerType,
        Type queryType);

    [LoggerMessage(
        EventId = EventIds.QueryHandlerThrownException,
        EventName = nameof(EventIds.QueryHandlerThrownException),
        Level = LogLevel.Error,
        Message = "Query handler of type {HandlerType} threw an exception while handling query of type {QueryType}.")]
    public static partial void QueryHandlerThrownException(
        this ILogger<Mediator> logger,
        Type handlerType,
        Type queryType,
        Exception exception);

    [LoggerMessage(
        EventId = EventIds.QueryHandlerReturnedResult,
        EventName = nameof(EventIds.QueryHandlerReturnedResult),
        Level = LogLevel.Trace,
        Message = "Query handler of type {HandlerType} returned result for query of type {QueryType}.")]
    public static partial void QueryHandlerReturnedResult(
        this ILogger<Mediator> logger,
        Type handlerType,
        Type queryType);

    private static class EventIds
    {
        public const int SendStarted = 1;

        public const int CommandHandlerNotFound = 2;

        public const int CallingCommandHandler = 3;

        public const int CommandHandlerThrownException = 4;

        public const int CommandHandlerReturnedResult = 5;

        public const int FetchStarted = 6;

        public const int QueryHandlerNotFound = 7;

        public const int CallingQueryHandler = 8;

        public const int QueryHandlerThrownException = 9;

        public const int QueryHandlerReturnedResult = 10;
    }
}

namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

internal static partial class HandlerFactoryLogs
{
    [LoggerMessage(
        EventId = EventIds.CreateCommandHandlerStarted,
        EventName = nameof(EventIds.CreateCommandHandlerStarted),
        Level = LogLevel.Trace,
        Message = "Started creating command handler for command of type {CommandType}.")]
    public static partial void CreateCommandHandlerStarted(
        this ILogger<HandlerFactory> logger,
        Type commandType);

    [LoggerMessage(
        EventId = EventIds.CommandHandlerNameFound,
        EventName = nameof(EventIds.CommandHandlerNameFound),
        Level = LogLevel.Trace,
        Message = "Command handler name for command of type {CommandType} was found: '{CommandHandlerName}'.")]
    public static partial void CommandHandlerNameFound(
        this ILogger<HandlerFactory> logger,
        Type commandType,
        string commandHandlerName);

    [LoggerMessage(
        EventId = EventIds.CommandHandlerNotFound,
        EventName = nameof(EventIds.CommandHandlerNotFound),
        Level = LogLevel.Warning,
        Message = "Command handler for command of type {CommandType} ('{CommandHandlerName}') was not found.")]
    public static partial void CommandHandlerNotFound(
        this ILogger<HandlerFactory> logger,
        Type commandType,
        string commandHandlerName);

    [LoggerMessage(
        EventId = EventIds.CommandHandlerFound,
        EventName = nameof(EventIds.CommandHandlerFound),
        Level = LogLevel.Trace,
        Message = "Command handler for command of type {CommandType} ('{CommandHandlerName}') was found.")]
    public static partial void CommandHandlerFound(
        this ILogger<HandlerFactory> logger,
        Type commandType,
        string commandHandlerName);

    [LoggerMessage(
        EventId = EventIds.CreateQueryHandlerStarted,
        EventName = nameof(EventIds.CreateQueryHandlerStarted),
        Level = LogLevel.Trace,
        Message = "Started creating query handler for query of type {QueryType}.")]
    public static partial void CreateQueryHandlerStarted(
        this ILogger<HandlerFactory> logger,
        Type queryType);

    [LoggerMessage(
        EventId = EventIds.QueryHandlerNameFound,
        EventName = nameof(EventIds.QueryHandlerNameFound),
        Level = LogLevel.Trace,
        Message = "Query handler name for query of type {QueryType} was found: '{QueryHandlerName}'.")]
    public static partial void QueryHandlerNameFound(
        this ILogger<HandlerFactory> logger,
        Type queryType,
        string queryHandlerName);

    [LoggerMessage(
        EventId = EventIds.QueryHandlerNotFound,
        EventName = nameof(EventIds.QueryHandlerNotFound),
        Level = LogLevel.Warning,
        Message = "Query handler for query of type {QueryType} ('{QueryHandlerName}') was not found.")]
    public static partial void QueryHandlerNotFound(
        this ILogger<HandlerFactory> logger,
        Type queryType,
        string queryHandlerName);

    [LoggerMessage(
        EventId = EventIds.QueryHandlerFound,
        EventName = nameof(EventIds.QueryHandlerFound),
        Level = LogLevel.Trace,
        Message = "Query handler for query of type {QueryType} ('{QueryHandlerName}') was found.")]
    public static partial void QueryHandlerFound(
        this ILogger<HandlerFactory> logger,
        Type queryType,
        string queryHandlerName);

    private static class EventIds
    {
        public const int CreateCommandHandlerStarted = 1;

        public const int CommandHandlerNameFound = 2;

        public const int CommandHandlerNotFound = 3;

        public const int CommandHandlerFound = 4;

        public const int CreateQueryHandlerStarted = 5;

        public const int QueryHandlerNameFound = 6;

        public const int QueryHandlerNotFound = 7;

        public const int QueryHandlerFound = 8;
    }
}

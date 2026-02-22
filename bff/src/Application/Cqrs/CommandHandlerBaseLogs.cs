namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

internal static partial class CommandHandlerBaseLogs
{
    [LoggerMessage(
        EventId = EventIds.HandleStarted,
        EventName = nameof(EventIds.HandleStarted),
        Level = LogLevel.Trace,
        Message = "Started handling of command of type '{CommandType}'.")]
    public static partial void HandleStarted(
        this ILogger<ICommandHandler> logger,
        Type commandType);

    [LoggerMessage(
        EventId = EventIds.NullCommand,
        EventName = nameof(EventIds.NullCommand),
        Level = LogLevel.Error,
        Message = "Received null command.")]
    public static partial void NullCommand(
        this ILogger<ICommandHandler> logger);

    [LoggerMessage(
        EventId = EventIds.InvalidCommandType,
        EventName = nameof(EventIds.InvalidCommandType),
        Level = LogLevel.Error,
        Message = "Invalid command type. Expected: '{ExpectedType}', Actual: '{ActualType}'.")]
    public static partial void InvalidCommandType(
        this ILogger<ICommandHandler> logger,
        Type expectedType,
        Type actualType);

    [LoggerMessage(
        EventId = EventIds.PassingToStronglyTypedHandle,
        EventName = nameof(EventIds.PassingToStronglyTypedHandle),
        Level = LogLevel.Trace,
        Message = "Passing command of type '{CommandType}' to strongly-typed handle method.")]
    public static partial void PassingToStronglyTypedHandle(
        this ILogger<ICommandHandler> logger,
        Type commandType);

    [LoggerMessage(
        EventId = EventIds.ExceptionOccurred,
        EventName = nameof(EventIds.ExceptionOccurred),
        Level = LogLevel.Error,
        Message = "An exception occurred while handling command of type '{CommandType}'.")]
    public static partial void ExceptionOccurred(
        this ILogger<ICommandHandler> logger,
        Type commandType,
        Exception exception);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed handling of command of type '{CommandType}'.")]
    public static partial void Completed(
        this ILogger<ICommandHandler> logger,
        Type commandType);

    private static class EventIds
    {
        public const int HandleStarted = 1001;

        public const int NullCommand = 1002;

        public const int InvalidCommandType = 1003;

        public const int PassingToStronglyTypedHandle = 1004;

        public const int ExceptionOccurred = 1005;

        public const int Completed = 1006;
    }
}

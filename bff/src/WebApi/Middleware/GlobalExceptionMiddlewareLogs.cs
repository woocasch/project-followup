namespace ProjectFollowUp.BFF.WebApi.Middleware;

internal static partial class GlobalExceptionMiddlewareLogs
{
    [LoggerMessage(
        EventId = EventIds.UnhandledException,
        EventName = nameof(EventIds.UnhandledException),
        Level = LogLevel.Error,
        Message = "An unhandled exception occurred while processing the request")]
    internal static partial void UnhandledException(this ILogger logger, Exception exception);

    private static class EventIds
    {
        public const int UnhandledException = 1001;
    }
}

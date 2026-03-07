namespace ProjectFollowUp.BFF.WebApi.Middleware;

internal static partial class GlobalExceptionMiddlewareLogs
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Error,
        Message = "An unhandled exception occurred while processing the request")]
    internal static partial void UnhandledException(this ILogger logger, Exception exception);
}

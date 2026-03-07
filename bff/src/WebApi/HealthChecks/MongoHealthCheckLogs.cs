namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

internal static partial class MongoHealthCheckLogs
{
    [LoggerMessage(
        EventId = EventIds.MongoHealthCheckFailed,
        EventName = nameof(EventIds.MongoHealthCheckFailed),
        Level = LogLevel.Error,
        Message = "MongoDB health check failed")]
    internal static partial void MongoHealthCheckFailed(this ILogger logger, Exception exception);

    private static class EventIds
    {
        public const int MongoHealthCheckFailed = 1002;
    }
}

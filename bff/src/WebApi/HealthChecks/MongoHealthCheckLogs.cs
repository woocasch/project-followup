namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

internal static partial class MongoHealthCheckLogs
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Error,
        Message = "MongoDB health check failed")]
    internal static partial void MongoHealthCheckFailed(this ILogger logger, Exception exception);
}

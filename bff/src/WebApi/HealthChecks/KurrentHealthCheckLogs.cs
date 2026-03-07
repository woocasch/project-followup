namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

internal static partial class KurrentHealthCheckLogs
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Error,
        Message = "KurrentDB health check failed")]
    internal static partial void KurrentHealthCheckFailed(this ILogger logger, Exception exception);
}

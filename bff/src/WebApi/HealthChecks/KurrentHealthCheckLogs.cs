namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

internal static partial class KurrentHealthCheckLogs
{
    [LoggerMessage(
        EventId = EventIds.KurrentHealthCheckFailed,
        EventName = nameof(EventIds.KurrentHealthCheckFailed),
        Level = LogLevel.Error,
        Message = "KurrentDB health check failed")]
    internal static partial void KurrentHealthCheckFailed(this ILogger logger, Exception exception);

    private static class EventIds
    {
        public const int KurrentHealthCheckFailed = 1;
    }
}

namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

internal static partial class RabbitMqHealthCheckLogs
{
    [LoggerMessage(
        EventId = EventIds.RabbitMqHealthCheckFailed,
        EventName = nameof(EventIds.RabbitMqHealthCheckFailed),
        Level = LogLevel.Error,
        Message = "RabbitMQ health check failed")]
    internal static partial void RabbitMqHealthCheckFailed(this ILogger logger, Exception exception);

    private static class EventIds
    {
        public const int RabbitMqHealthCheckFailed = 1001;
    }
}

namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

internal static partial class RabbitMqHealthCheckLogs
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Error,
        Message = "RabbitMQ health check failed")]
    internal static partial void RabbitMqHealthCheckFailed(this ILogger logger, Exception exception);
}

namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Infrastructure.EventBus;

using RabbitMQ.Client;

public sealed class RabbitMqHealthCheck(
    IOptions<EventBusSettings> eventBusSettings,
    ILogger<RabbitMqHealthCheck> logger) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(eventBusSettings.Value.ConnectionString)
            };

            var connection = await factory.CreateConnectionAsync(cancellationToken);
            await using (connection.ConfigureAwait(false))
            {
                var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
                await using (channel.ConfigureAwait(false))
                {
                    return HealthCheckResult.Healthy("RabbitMQ is reachable");
                }
            }
        }
        catch (Exception ex)
        {
            logger.RabbitMqHealthCheckFailed(ex);
            return HealthCheckResult.Unhealthy("RabbitMQ is not reachable", ex);
        }
    }
}

namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

using KurrentDB.Client;

using Microsoft.Extensions.Diagnostics.HealthChecks;

public sealed class KurrentHealthCheck(KurrentDBClient client, ILogger<KurrentHealthCheck> logger) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await client.ReadAllAsync(
                Direction.Forwards,
                Position.Start,
                maxCount: 1,
                cancellationToken: cancellationToken).ToArrayAsync(cancellationToken);

            return HealthCheckResult.Healthy("KurrentDB is reachable");
        }
        catch (Exception ex)
        {
            logger.KurrentHealthCheckFailed(ex);
            return HealthCheckResult.Unhealthy("KurrentDB is not reachable", ex);
        }
    }
}

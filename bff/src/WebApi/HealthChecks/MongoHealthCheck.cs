namespace ProjectFollowUp.BFF.WebApi.HealthChecks;

using Microsoft.Extensions.Diagnostics.HealthChecks;

using MongoDB.Driver;

public sealed class MongoHealthCheck(IMongoClient client, ILogger<MongoHealthCheck> logger) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await client.ListDatabaseNamesAsync(cancellationToken);
            return HealthCheckResult.Healthy("MongoDB is reachable");
        }
        catch (Exception ex)
        {
            logger.MongoHealthCheckFailed(ex);
            return HealthCheckResult.Unhealthy("MongoDB is not reachable", ex);
        }
    }
}

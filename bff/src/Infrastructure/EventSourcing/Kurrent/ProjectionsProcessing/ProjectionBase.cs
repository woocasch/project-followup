namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using System.Reflection;

using KurrentDB.Client;

public abstract class ProjectionBase(
    KurrentDBProjectionManagementClient client,
    string projectionName,
    Assembly resourcesContainer,
    string resourceName) : IProjection
{
    public async Task CreateOrUpdate(CancellationToken cancellationToken)
    {
        if (await this.ProjectionExists())
        {
            await client.UpdateAsync(
                projectionName,
                this.GetProjectionQuery(),
                emitEnabled: true,
                cancellationToken: cancellationToken);
        }
        else
        {
            var projectionQuery = this.GetProjectionQuery();
            await client.CreateContinuousAsync(
                projectionName,
                projectionQuery,
                cancellationToken: cancellationToken);
            await client.UpdateAsync(
                projectionName,
                projectionQuery,
                emitEnabled: true,
                cancellationToken: cancellationToken);
        }
    }

    private string GetProjectionQuery()
    {
        using var stream = resourcesContainer.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found in '{resourcesContainer.FullName}'.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private async Task<bool> ProjectionExists()
    {
        var allProjectionsAsync = client.ListAllAsync();
        var allProjections = await allProjectionsAsync.ToListAsync();
        return allProjections.Any(p => p.Name == projectionName);
    }
}

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
            await client.UpdateAsync(projectionName, this.GetProjectionContent(), cancellationToken: cancellationToken);
        }
        else
        {
            await client.CreateContinuousAsync(projectionName, this.GetProjectionContent(), cancellationToken: cancellationToken);
        }
    }

    private string GetProjectionContent()
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

namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using KurrentDB.Client;

public sealed class UsersProjection(
    KurrentDBProjectionManagementClient client)
{
    public async Task CreateProjection(CancellationToken cancellationToken)
    {
        var projectionText = GetProjectionContent();
        var allProjectionsAsync = client.ListAllAsync(cancellationToken: cancellationToken);
        var allProjections = await allProjectionsAsync.ToListAsync();
        if (allProjections.Any(p => p.Name == "Users"))
        {
            await client.UpdateAsync("Users", projectionText, cancellationToken: cancellationToken);
        }
        else
        {
            await client.CreateContinuousAsync("Users", projectionText, cancellationToken: cancellationToken);
        }
    }

    private static string GetProjectionContent()
    {
        var resourceName = "ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing.Users.js";
        using var stream = typeof(UsersProjection).Assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}

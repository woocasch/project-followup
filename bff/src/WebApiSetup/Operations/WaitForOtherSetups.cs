namespace ProjectFollowUp.BFF.WebApiSetup.Operations;

using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;

using Microsoft.Extensions.Options;

public sealed class WaitForOtherSetups(
    IReporter reporter,
    KeycloakClient keycloakClient,
    IOptions<SetupSettings> settings) : IOperation
{
    public int Order => 1;

    public string Description => "Waiting for storage setup to be completed";

    private SetupSettings Settings => settings.Value;

    public async Task Execute(CancellationToken cancellationToken)
    {
        var otherSetupsCompleted = false;
        do
        {
            var checks = this.GetChecks().ToArray();
            await Task.WhenAll(checks);
            otherSetupsCompleted = checks.All(c => c.Result);
            if (!otherSetupsCompleted)
            {
                reporter.Warning("Some checks are not completed yet. Waiting for 5 seconds before checking again...");
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        } while (!otherSetupsCompleted);
    }

    public Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }

    private async Task<bool> IsAdminUserCreated(string realmId, string userName, CancellationToken cancellationToken)
    {
        var users = await keycloakClient.GetUsersAsync(
            realmId,
            username: userName,
            cancellationToken: cancellationToken);
        return users?.FirstOrDefault() != null;
    }

    private IEnumerable<Task<bool>> GetChecks()
    {
        yield return this.IsAdminUserCreated(this.Settings.ApplicationRealm, "projectfollowup", CancellationToken.None);
    }
}

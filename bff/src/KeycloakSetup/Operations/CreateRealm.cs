namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;

using Microsoft.Extensions.Options;

public sealed class CreateRealm(
    IOptions<SetupSettings> setupSettingsOptions,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private readonly SetupSettings setupSettings = setupSettingsOptions.Value;

    public int Order => 1;

    public string Description => $"Create Keycloak realm '{setupSettings.ProjectFollowUpRealm.RealmName}'.";

    public async Task Execute(CancellationToken cancellationToken)
    {
        var created = await keycloakClient.CreateRealm(
            setupSettings.ProjectFollowUpRealm.RealmId,
            setupSettings.ProjectFollowUpRealm.RealmName,
            cancellationToken);
        if (!created)
        {
            var message = $"Failed to create Keycloak realm '{setupSettings.ProjectFollowUpRealm.RealmName}'.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Realm '{setupSettings.ProjectFollowUpRealm.RealmName}' created.");
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var realm = await keycloakClient.FindRealm(setupSettings.ProjectFollowUpRealm.RealmId, cancellationToken);
        return realm is null;
    }
}

namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;

using Microsoft.Extensions.Options;

public sealed class CreateUIClient(
    IOptions<SetupSettings> setupSettingsOptions,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private readonly SetupSettings setupSettings = setupSettingsOptions.Value;

    public int Order => 2;

    public string Description => "Creating a client for UI in realm";

    public async Task Execute(CancellationToken cancellationToken)
    {
        var clientSetup = setupSettings.ProjectFollowUpRealm.UIClient;
        var result = await keycloakClient.CreateClient(
            setupSettings.ProjectFollowUpRealm.RealmId,
            clientSetup.ClientId,
            clientSetup.DisplayName,
            clientSetup.RedirectUrls,
            clientSetup.WebOrigins,
            KeycloakClientExtensions.FlowType.Standard,
            cancellationToken);
        if (!result)
        {
            var message = $"Failed to create client '{clientSetup.ClientId}'";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Client '{clientSetup.ClientId}' created");
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var client = await keycloakClient.FindClient(
            setupSettings.ProjectFollowUpRealm.RealmId,
            setupSettings.ProjectFollowUpRealm.UIClient.ClientId,
            cancellationToken);
        return client is null;
    }
}

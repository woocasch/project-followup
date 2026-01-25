namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;

using Microsoft.Extensions.Options;

public sealed class CreateUIClient(
    IOptions<RealmSettings> realmSettings,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private const string FrontendClientId = "frontend";

    public int Order => 2;

    public string Description => "Creating a client for UI in realm";

    public async Task Execute(CancellationToken cancellationToken)
    {
        await keycloakClient.CreateClientAsync(
            realmSettings.Value.RealmId,
            new()
            {
                ClientId = FrontendClientId,
                Name = "Frontend",
                Enabled = true,
                PublicClient = false,
                StandardFlowEnabled = true,
                DirectAccessGrantsEnabled = false,
                ServiceAccountsEnabled = false,
                AuthorizationServicesEnabled = false,
                RedirectUris =
                [
                    "https://localhost:4000/*",
                ],
                WebOrigins =
                [
                    "https://localhost:4000",
                ],
            },
            cancellationToken);
        reporter.Info($"Client '{FrontendClientId}' created");
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var client = await this.FindClient(cancellationToken);
        return client is null;
    }

    private async Task<Client?> FindClient(CancellationToken cancellationToken)
    {
        var clients = await keycloakClient.GetClientsAsync(
            realmSettings.Value.RealmId,
            clientId: FrontendClientId,
            cancellationToken: cancellationToken);
        var client = clients.SingleOrDefault();
        return client;
    }
}

namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;

using Microsoft.Extensions.Options;

public sealed class CreateBffClient(
    IOptions<RealmSettings> realmSettings,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private const string WebApiBffClientId = "webapi-bff";

    public string Description => "Creating a client for WebAPI BFF in realm";

    public async Task Execute(CancellationToken cancellationToken)
    {
        await keycloakClient.CreateClientAsync(
            realmSettings.Value.RealmId,
            new()
            {
                ClientId = WebApiBffClientId,
                Name = "WebAPI - BFF",
                Enabled = true,
                PublicClient = false,
                StandardFlowEnabled = true,
                DirectAccessGrantsEnabled = false,
                ServiceAccountsEnabled = true,
                AuthorizationServicesEnabled = true,
            },
            cancellationToken);
        reporter.Info($"Client '{WebApiBffClientId}' created");
        var client = await this.FindClient(cancellationToken);
        if (client is null)
        {
            var message = $"Client '{WebApiBffClientId}' was not found after creation.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Getting realm-management client id.");
        var realmManagementClient = await this.FindClient(
            "realm-management",
            cancellationToken);
        if (realmManagementClient is null)
        {
            var message = $"realm-management client was not found.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Creating required roles.");
        var rolesToAssign = new[]
        {
            "view-users",
            "manage-users",
        };
        var roles = await keycloakClient.GetRolesAsync(
            realmSettings.Value.RealmId,
            realmManagementClient.Id,
            cancellationToken: cancellationToken);
        var rolesForAssignment = roles
            .Where(r => rolesToAssign.Contains(r.Name))
            .ToList();
        reporter.Info("Getting service account user");
        var serviceAccountUser = await keycloakClient.GetUserForServiceAccountAsync(
            realmSettings.Value.RealmId,
            client.Id,
            cancellationToken);
        if (serviceAccountUser is null)
        {
            var message = "Service account user not found for the client";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Assigning service roles to service account user.");
        await keycloakClient.AddClientRoleMappingsToUserAsync(
            realmSettings.Value.RealmId,
            serviceAccountUser.Id,
            client.Id,
            rolesForAssignment,
            cancellationToken);
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        //var client = await this.FindClient(cancellationToken);
        //return client is null;
        return true;
    }

    private async Task<Client?> FindClient(CancellationToken cancellationToken)
    {
        return await this.FindClient(WebApiBffClientId, cancellationToken);
    }

    private async Task<Client?> FindClient(string clientName, CancellationToken cancellationToken)
    {
        var clients = await keycloakClient.GetClientsAsync(
            realmSettings.Value.RealmId,
            clientId: clientName,
            cancellationToken: cancellationToken);
        var client = clients.SingleOrDefault();
        return client;
    }
}

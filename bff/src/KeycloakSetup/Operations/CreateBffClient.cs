namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;

using Microsoft.Extensions.Options;

public sealed class CreateBffClient(
    IOptions<SetupSettings> setupSettingsOptions,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private readonly SetupSettings setupSettings = setupSettingsOptions.Value;

    public int Order => 3;

    public string Description => "Creating a client for WebAPI BFF in realm";

    public async Task Execute(CancellationToken cancellationToken)
    {
        var clientSetup = setupSettings.ProjectFollowUpRealm.WebApiBffClient;
        var client = await this.CreateClient(clientSetup, cancellationToken);

        await this.AssignClientRoles(client, cancellationToken);

        var result = await this.CreateClientSecret(client, cancellationToken);
        reporter.Warning("=============================================");
        reporter.Warning("A client secret was generated for the BFF client. Make sure to store it securely.");
        reporter.Warning($"Client secret: {result}");
        reporter.Warning("This message will not be written again.");
        reporter.Warning("=============================================");
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var client = await keycloakClient.FindClient(
            setupSettings.ProjectFollowUpRealm.RealmId,
            setupSettings.ProjectFollowUpRealm.WebApiBffClient.ClientId,
            cancellationToken);
        return client is null;
    }

    private async Task<Client> FindClient(string clientId, CancellationToken cancellationToken)
    {
        var client = await keycloakClient.FindClient(
            setupSettings.ProjectFollowUpRealm.RealmId,
            clientId,
            cancellationToken);
        if (client is null)
        {
            var message = $"Client '{clientId}' was not found after creation.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        return client;
    }

    private async Task<Client> CreateClient(SetupSettings.ProjectFollowUpRealmSettings.ClientSettings clientSetup, CancellationToken cancellationToken)
    {
        var clientCreated = await keycloakClient.CreateClient(
                    setupSettings.ProjectFollowUpRealm.RealmId,
                    clientSetup.ClientId,
                    clientSetup.DisplayName,
                    clientSetup.RedirectUrls,
                    clientSetup.WebOrigins,
                    KeycloakClientExtensions.FlowType.ServiceAccount,
                    cancellationToken);
        if (!clientCreated)
        {
            var message = $"Failed to create client '{clientSetup.ClientId}'";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Client '{clientSetup.ClientId}' created");

        return await this.FindClient(
            clientSetup.ClientId,
            cancellationToken);
    }

    private async Task AssignClientRoles(Client client, CancellationToken cancellationToken)
    {
        reporter.Info($"Getting realm-management client id.");
        var realmManagementClient = await this.FindClient(
            "realm-management",
            cancellationToken);

        reporter.Info($"Creating required roles.");
        var rolesToAssign = new[]
        {
            "view-users",
            "manage-users",
            "query-users",
        };
        var rolesForAssignment = (await keycloakClient.FindClientRoles(
            setupSettings.ProjectFollowUpRealm.RealmId,
            realmManagementClient.Id,
            rolesToAssign,
            cancellationToken))
            .ToList();

        reporter.Info("Getting service account user");
        var serviceAccountUser = await keycloakClient.FindServiceAccountUser(
            setupSettings.ProjectFollowUpRealm.RealmId,
            client.Id,
            cancellationToken);
        if (serviceAccountUser is null)
        {
            var message = "Service account user not found for the client";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        reporter.Info($"Assigning service roles to service account user.");
        await keycloakClient.AssignRolesToUser(
            setupSettings.ProjectFollowUpRealm.RealmId,
            serviceAccountUser.Id,
            realmManagementClient.Id,
            rolesForAssignment,
            cancellationToken);
    }

    private async Task<string> CreateClientSecret(Client client, CancellationToken cancellationToken)
    {
        return await keycloakClient.RegenerateClientSecret(
            setupSettings.ProjectFollowUpRealm.RealmId,
            client.Id,
            cancellationToken);
    }
}

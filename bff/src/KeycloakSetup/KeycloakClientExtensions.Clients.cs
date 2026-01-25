namespace ProjectFollowUp.BFF.KeycloakSetup;

using System.Threading;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.Users;

public static partial class KeycloakClientExtensions
{
    public static async Task<Client?> FindClient(
        this KeycloakClient keycloakClient,
        string realmId,
        string clientId,
        CancellationToken cancellationToken)
    {
        var clients = await keycloakClient.GetClientsAsync(
            realmId,
            clientId: clientId,
            cancellationToken: cancellationToken);
        var result = clients.SingleOrDefault();
        return result;
    }

    public static async Task<bool> CreateClient(
        this KeycloakClient keycloakClient,
        string realmId,
        string clientId,
        string name,
        string[] redirectUrls,
        string[] webOrigins,
        FlowType flowTypes,
        CancellationToken cancellationToken)
    {
        var isStandardFlowEnabled = (flowTypes & FlowType.Standard) == FlowType.Standard;
        var isServiceAccountEnabled = (flowTypes & FlowType.ServiceAccount) == FlowType.ServiceAccount;
        return await keycloakClient.CreateClientAsync(
            realmId,
            new()
            {
                ClientId = clientId,
                Name = name,
                Enabled = true,
                PublicClient = false,
                StandardFlowEnabled = isStandardFlowEnabled,
                DirectAccessGrantsEnabled = false,
                ServiceAccountsEnabled = isServiceAccountEnabled,
                AuthorizationServicesEnabled = isServiceAccountEnabled,
                RedirectUris = redirectUrls,
                WebOrigins = webOrigins,
            },
            cancellationToken);
    }

    public static async Task<User?> FindServiceAccountUser(
        this KeycloakClient keycloakClient,
        string realmId,
        string clientId,
        CancellationToken cancellationToken)
    {
        var serviceAccountUser = await keycloakClient.GetUserForServiceAccountAsync(
            realmId,
            clientId,
            cancellationToken);
        return serviceAccountUser;
    }

    public static async Task<string> RegenerateClientSecret(
        this KeycloakClient keycloakClient,
        string realmId,
        string clientId,
        CancellationToken cancellationToken)
    {
        var secret = await keycloakClient.GenerateClientSecretAsync(
            realmId,
            clientId,
            cancellationToken);
        return secret.Value;
    }

    [Flags]
    public enum FlowType
    {
        Standard = 1,
        ServiceAccount = 1 >> 1,
    }
}

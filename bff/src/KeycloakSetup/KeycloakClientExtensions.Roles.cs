namespace ProjectFollowUp.BFF.KeycloakSetup;

using System.Threading;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.Roles;

public static partial class KeycloakClientExtensions
{
    public static async Task<IEnumerable<Role>> FindClientRoles(
        this KeycloakClient keycloakClient,
        string realmId,
        string clientId,
        string[] roles,
        CancellationToken cancellationToken)
    {
        var clientRoles = await keycloakClient.GetRolesAsync(
            realmId,
            clientId,
            cancellationToken: cancellationToken);
        return clientRoles
            .Where(r => roles.Contains(r.Name));
    }
}

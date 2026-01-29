namespace ProjectFollowUp.BFF.KeycloakSetup;

using System.Threading;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.Roles;

public static partial class KeycloakClientExtensions
{
    public static async Task<bool> CreateRole(
        this KeycloakClient keycloakClient,
        string realmId,
        string roleId,
        string roleDescription,
        CancellationToken cancellationToken)
    {
        return await keycloakClient.CreateRoleAsync(
            realmId,
            new Role
            {
                Name = roleId,
                Description = roleDescription
            },
            cancellationToken);
    }

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

    public static async Task<IEnumerable<Role>> GetAllRealmRoles(
        this KeycloakClient keycloakClient,
        string realmId,
        CancellationToken cancellationToken)
    {
        return await keycloakClient.GetRolesAsync(
            realmId,
            cancellationToken: cancellationToken);
    }

    public static async Task<IEnumerable<Role>> FindRealmRoles(
        this KeycloakClient keycloakClient,
        string realmId,
        string[] roles,
        CancellationToken cancellationToken)
    {
        var realmRoles = await keycloakClient.GetRolesAsync(
            realmId,
            cancellationToken: cancellationToken);
        return realmRoles
            .Where(r => roles.Contains(r.Name));
    }
}

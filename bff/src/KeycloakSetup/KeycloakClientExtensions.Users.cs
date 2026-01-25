namespace ProjectFollowUp.BFF.KeycloakSetup;

using System.Threading;

using Keycloak.Net;
using Keycloak.Net.Models.Clients;
using Keycloak.Net.Models.Roles;
using Keycloak.Net.Models.Users;

public static partial class KeycloakClientExtensions
{
    public static async Task<bool> CreateUser(
        this KeycloakClient keycloakClient,
        string realmId,
        string userName,
        string email,
        string password,
        CancellationToken cancellationToken)
    {
        var user = new User()
        {
            UserName = userName,
            Enabled = true,
            Email = email,
            EmailVerified = true,
            Credentials =
            [
                new Credentials()
                {
                    Type = "password",
                    Value = password,
                    Temporary = true,
                }
            ],
        };
        return await keycloakClient.CreateUserAsync(
            realmId,
            user,
            cancellationToken);
    }

    public static async Task<User?> GetUserByUsername(
        this KeycloakClient keycloakClient,
        string realmId,
        string userName,
        CancellationToken cancellationToken)
    {
        var users = await keycloakClient.GetUsersAsync(
            realmId,
            username: userName,
            cancellationToken: cancellationToken);
        return users?.FirstOrDefault();
    }

    public static async Task<bool> AssignRolesToUser(
        this KeycloakClient keycloakClient,
        string realmId,
        string userId,
        string rolesSourceClientId,
        IEnumerable<Role> roles,
        CancellationToken cancellationToken)
    {
        return await keycloakClient.AddClientRoleMappingsToUserAsync(
            realmId,
            userId,
            rolesSourceClientId,
            roles,
            cancellationToken);
    }
}

namespace ProjectFollowUp.BFF.KeycloakSetup;

using Keycloak.Net;
using Keycloak.Net.Models.RealmsAdmin;

public static partial class KeycloakClientExtensions
{
    public static async Task<Realm?> FindRealm(
        this KeycloakClient keycloakClient, 
        string realmId,
        CancellationToken cancellationToken)
    {
        var realms = await keycloakClient.GetRealmsAsync("master", cancellationToken);
        return realms.SingleOrDefault(r => r._Realm == realmId);
    }

    public static async Task<bool> CreateRealm(
        this KeycloakClient keycloakClient,
        string id,
        string displayName,
        CancellationToken cancellationToken)
    {
        return await keycloakClient.ImportRealmAsync(
            "master",
            new()
            {
                _Realm = id,
                DisplayName = displayName,
                LoginWithEmailAllowed = true,
                DuplicateEmailsAllowed = false,
                EditUsernameAllowed = false,
                VerifyEmail = true,
            },
            cancellationToken);
    }
}

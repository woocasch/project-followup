namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;

using Microsoft.Extensions.Options;

public sealed class CreateRealm(
    IOptions<RealmSettings> realmSettings,
    KeycloakClient keycloakClient) : IOperation
{
    public string Description => $"Create Keycloak realm '{realmSettings.Value.RealmName}'.";

    public async Task Execute(CancellationToken cancellationToken)
    {
        await keycloakClient.ImportRealmAsync(
            "master",
            new()
            {
                _Realm = realmSettings.Value.RealmId,
                DisplayName = realmSettings.Value.RealmName,
                LoginWithEmailAllowed = true,
                DuplicateEmailsAllowed = false,
                EditUsernameAllowed = false,
                VerifyEmail = true,
            },
            cancellationToken);
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var realms = await keycloakClient.GetRealmsAsync("master", cancellationToken);
        var ourRealm = realms.SingleOrDefault(r => r._Realm == realmSettings.Value.RealmId);
        return ourRealm is null;
    }
}

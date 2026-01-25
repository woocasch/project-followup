namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;
using Keycloak.Net.Models.Users;

using Microsoft.Extensions.Options;

public sealed class CreateAdminUser(
    IOptions<SetupSettings> realmSettings,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    public int Order => 4;

    public string Description => "Creating application's admin user";

    public async Task Execute(CancellationToken cancellationToken)
    {
        var result = await keycloakClient.CreateUser(
            realmSettings.Value.ProjectFollowUpRealm.RealmId,
            "projectfollowup",
            "admin@projectfollowup.dev",
            "projectfollowup",
            cancellationToken);
        if (!result)
        {
            var message = "Failed to create admin user.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var user = await keycloakClient.GetUserByUsername(
            realmSettings.Value.ProjectFollowUpRealm.RealmId,
            "projectfollowup",
            cancellationToken: cancellationToken);
        return user is null;
    }
}

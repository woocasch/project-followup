namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;
using Keycloak.Net.Models.Users;

using Microsoft.Extensions.Options;

public sealed class CreateAdminUser(
    IOptions<RealmSettings> realmSettings,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    public int Order => 4;

    public string Description => "Creating application's admin user";

    public async Task Execute(CancellationToken cancellationToken)
    {
        var user = new User()
        {
            UserName = "projectfollowup",
            Enabled = true,
            Email = "admin@projectfollowup.dev",
            EmailVerified = true,
            Credentials =
            [
                new Credentials()
                {
                    Type = "password",
                    Value = "projectfollowup",
                    Temporary = true,
                }
            ],
        };

        var result = await keycloakClient.CreateUserAsync(
            realmSettings.Value.RealmId,
            user,
            cancellationToken);
        if (!result)
        {
            var message = "Failed to create admin user.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        var createdUser = (await keycloakClient.GetUsersAsync(
            realmSettings.Value.RealmId,
            username: "projectfollowup",
            cancellationToken: cancellationToken))
            .FirstOrDefault();
        if (createdUser is null)
        {
            var message = "Admin user was not found after creation.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var user = await keycloakClient.GetUsersAsync(
            realmSettings.Value.RealmId,
            username: "projectfollowup",
            cancellationToken: cancellationToken);
        return !user.Any();
    }
}

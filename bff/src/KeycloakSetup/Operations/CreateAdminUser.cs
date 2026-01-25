namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;
using Keycloak.Net.Models.Users;

using Microsoft.Extensions.Options;

public sealed class CreateAdminUser(
    IOptions<SetupSettings> setupSettingsOptions,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private readonly SetupSettings setupSettings = setupSettingsOptions.Value;

    public int Order => 5;

    public string Description => "Creating application's admin user";

    public async Task Execute(CancellationToken cancellationToken)
    {
        var result = await keycloakClient.CreateUser(
            setupSettings.ProjectFollowUpRealm.RealmId,
            setupSettings.ProjectFollowUpRealm.AdminUser.Username,
            setupSettings.ProjectFollowUpRealm.AdminUser.Email,
            setupSettings.ProjectFollowUpRealm.AdminUser.Password,
            cancellationToken);
        if (!result)
        {
            var message = "Failed to create admin user.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        string[] adminRoleNames = [
            "create-users",
            "search-users",
            ];
        var adminRoles = await keycloakClient.FindRealmRoles(
            setupSettings.ProjectFollowUpRealm.RealmId,
            adminRoleNames,
            cancellationToken);
        var user = await keycloakClient.GetUserByUsername(
            setupSettings.ProjectFollowUpRealm.RealmId,
            setupSettings.ProjectFollowUpRealm.AdminUser.Username,
            cancellationToken: cancellationToken);
        if (user is null)
        {
            var message = "Admin user was not found after creation.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }

        var assigned = await keycloakClient.AssignRolesToUser(
            setupSettings.ProjectFollowUpRealm.RealmId,
            user.Id!,
            adminRoles,
            cancellationToken);
        if (!assigned)
        {
            var message = $"Failed to assign admin roles to user '{setupSettings.ProjectFollowUpRealm.AdminUser.Username}'.";
            reporter.Error(message);
            throw new InvalidOperationException(message);
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var user = await keycloakClient.GetUserByUsername(
            setupSettings.ProjectFollowUpRealm.RealmId,
            "projectfollowup",
            cancellationToken: cancellationToken);
        return user is null;
    }
}

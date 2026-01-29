namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using Keycloak.Net;

using Microsoft.Extensions.Options;

public sealed class CreateCustomRoles(
    IOptions<SetupSettings> setupSettingsOptions,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private readonly IReadOnlyDictionary<string, string> roles = new Dictionary<string, string>
    {
        { "create-user", "Can create application users." },
        { "search-user", "Can search for application users." },
    };

    public int Order => 2;

    public string Description => "Create roles in the realm";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating missing roles");
        foreach (var role in this.roles)
        {
            var created = await this.CreateRoleIfNeeded(role.Key, role.Value, cancellationToken);
            if (!created)
            {
                var message = $"Role '{role.Key}' was not created.";
                reporter.Error(message);
                throw new InvalidOperationException(message);
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var existingRoles = (await keycloakClient.GetAllRealmRoles(
            setupSettingsOptions.Value.ProjectFollowUpRealm.RealmId,
            cancellationToken))
            .ToList();
        
        return roles.Keys.Any(role => !existingRoles.Any(r => r.Name == role));
    }

    private async Task<bool> CreateRoleIfNeeded(string roleName, string roleDescription, CancellationToken cancellationToken)
    {
        var existingRoles = (await keycloakClient.GetAllRealmRoles(
            setupSettingsOptions.Value.ProjectFollowUpRealm.RealmId,
            cancellationToken))
            .ToList();
        if (existingRoles.Any(r => r.Name == roleName))
        {
            return true;
        }

        var created = await keycloakClient.CreateRole(
            setupSettingsOptions.Value.ProjectFollowUpRealm.RealmId,
            roleName,
            roleDescription,
            cancellationToken);
        if (!created)
        {
            return false;
        }

        reporter.Info($"Role '{roleName}' created.");
        return true;
    }
}

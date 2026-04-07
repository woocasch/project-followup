namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Text.Json;
using System.Text.Json.Nodes;

using Flurl;
using Flurl.Http;

using Keycloak.Net;
using Keycloak.Net.Common.Extensions;
using Keycloak.Net.Models.ProtocolMappers;
using Keycloak.Net.Models.RealmsAdmin;

using Microsoft.Extensions.Options;

public sealed class CreateUserIdAttribute(
    IOptions<SetupSettings> setupSettingsOptions,
    KeycloakClient keycloakClient,
    IReporter reporter) : IOperation
{
    private const string AttributeName = "projectfollowup-userid";

    private const string MapperName = "ProjectFollowUpUserIdMapper";

    public int Order => 3;

    public string Description => "Create roles in the realm";

    private SetupSettings Settings => setupSettingsOptions.Value;

    public async Task Execute(CancellationToken cancellationToken)
    {
        if (!await this.HasUserIdAttribute(Settings.ProjectFollowUpRealm.RealmId, AttributeName, cancellationToken))
        {
            reporter.Info("Creating user id attribute on user profile config.");
            await this.AddUserIdAttribute(
            Settings.ProjectFollowUpRealm.RealmId,
            AttributeName,
            cancellationToken);
        }
        else
        {
            reporter.Info("User id attribute already exists.");
        }

        var existing = await this.GetProtocolMapper(Settings.ProjectFollowUpRealm.RealmId, "profile", MapperName, cancellationToken);
        if (existing is null)
        {
            reporter.Info("Creating mapper for user id attribute.");
            await this.CreateProtocolMapper(Settings.ProjectFollowUpRealm.RealmId, "profile", new ProtocolMapper
            {
                Name = MapperName,
                Protocol = "openid-connect",
                _ProtocolMapper = "oidc-usermodel-attribute-mapper",
                Config = new Dictionary<string, string>
                {
                    ["user.attribute"] = AttributeName,
                    ["claim.name"] = AttributeName,
                    ["jsonType.label"] = "String",
                    ["id.token.claim"] = "true",
                    ["access.token.claim"] = "true",
                    ["userinfo.token.claim"] = "true"
                }
            }, cancellationToken);
        }
        else
        {
            reporter.Info("Mapper for user id attribute already exists.");
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        if (!await this.HasUserIdAttribute(Settings.ProjectFollowUpRealm.RealmId, AttributeName, cancellationToken))
        {
            reporter.Info($"Attribute '{AttributeName}' was not found on user profile config.");
            return true;
        }

        var mapper = await this.GetProtocolMapper(Settings.ProjectFollowUpRealm.RealmId, "profile", MapperName, cancellationToken);
        return mapper is null;
    }

    private async Task<ProtocolMapper?> GetProtocolMapper(string realmId, string clientScopeId, string mapperName, CancellationToken cancellationToken)
    {
        var clientScopes = (await keycloakClient.GetClientScopesAsync(
            Settings.ProjectFollowUpRealm.RealmId,
            cancellationToken)).ToList();
        var scope = clientScopes.Single(s => s.Name == clientScopeId);
        var mapper = scope.ProtocolMappers.SingleOrDefault(m => m.Name == mapperName);
        return mapper;
    }

    private async Task CreateProtocolMapper(string realmId, string clientScopeId, ProtocolMapper protocolMapper, CancellationToken cancellationToken)
    {
        var clientScopes = (await keycloakClient.GetClientScopesAsync(
            Settings.ProjectFollowUpRealm.RealmId,
            cancellationToken)).ToList();
        var scope = clientScopes.Single(s => s.Name == clientScopeId);
        await keycloakClient.CreateProtocolMapperAsync(
            realmId,
            scope.Id,
            protocolMapper,
            cancellationToken);
    }

    private async Task<(IFlurlResponse UserProfileDataResponse, JsonArray Attributes, JsonObject ProfileObject)> GetUserProfileConfig(string realmId, CancellationToken cancellationToken)
    {
        var userProfileDataResponse = await GetUserProfile(realmId, cancellationToken);
        var responseContent = await userProfileDataResponse.GetStringAsync();
        var userProfileConfig = JsonNode.Parse(responseContent);
        if (userProfileConfig is not JsonObject profileObject)
        {
            reporter.Error("User profile config is not a JSON object");
            throw new InvalidOperationException("User profile config is not a valid JSON object");
        }
        if (profileObject["attributes"] is not JsonArray attributes)
        {
            reporter.Error("Attributes is not an array");
            throw new InvalidOperationException("Attributes were not found on user profile config");
        }
        return (userProfileDataResponse, attributes, profileObject);
    }

    private async Task<bool> HasUserIdAttribute(string realmId, string attributeName, CancellationToken cancellationToken)
    {
        (var _, var attributes, var profileObject) = await GetUserProfileConfig(realmId, cancellationToken);
        var expectedAttributeName = attributeName.ToLowerInvariant();
        var hasAttribute = attributes
            .Any(a => a?["name"]?.GetValue<string>()?.ToLowerInvariant() == expectedAttributeName);
        return hasAttribute;
    }

    private async Task AddUserIdAttribute(string realmId, string attributeName, CancellationToken cancellationToken)
    {
        (var _, var attributes, var profileObject) = await GetUserProfileConfig(realmId, cancellationToken);
        var expectedAttributeName = attributeName.ToLowerInvariant();
        var hasAttribute = attributes
            .Any(a => a?["name"]?.GetValue<string>()?.ToLowerInvariant() == expectedAttributeName);
        if (hasAttribute)
        {
            return;
        }

        var newAttribute = new JsonObject
        {
            ["name"] = attributeName,
            ["displayName"] = "User id in ProjectFollowUp ecosystem.",
            ["validations"] = new JsonObject(),
            ["annotations"] = new JsonObject(),
            ["permissions"] = new JsonObject
            {
                ["view"] = new JsonArray(),
                ["edit"] = new JsonArray("admin")
            },
            ["group"] = "user-metadata",
            ["multivalued"] = false
        };

        attributes.Add(newAttribute);

        await UpdateUserProfile(realmId, profileObject, cancellationToken);
    }

    private static async Task UpdateUserProfile(string realmId, JsonObject profileObject, CancellationToken cancellationToken)
    {
        await new Url("http://localhost:4002")
            .AppendPathSegment($"/admin/realms/{realmId}/users/profile")
            .WithSettings(s => { })
            .WithAuthentication(null, "http://localhost:4002", "master", "admin", "admin", null)
            .PutJsonAsync(profileObject, cancellationToken: cancellationToken);
    }

    private static async Task<IFlurlResponse> GetUserProfile(string realmId, CancellationToken cancellationToken)
    {
        return await new Url("http://localhost:4002")
            .AppendPathSegment($"/admin/realms/{realmId}/users/profile")
            .WithSettings(s => { })
            .WithAuthentication(null, "http://localhost:4002", "master", "admin", "admin", null)
            .GetAsync(HttpCompletionOption.ResponseContentRead, cancellationToken);
    }
}

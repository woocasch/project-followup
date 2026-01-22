namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Application.IdentityProvider;

public sealed class KeycloakIdentityProvider(
    IOptions<KeycloakSettings> options,
    global::Keycloak.Net.KeycloakClient client) : IIdentityProvider
{
    public async Task<CreateUserCredentialsResponse> CreateUserCredentials(
        CreateUserCredentialsRequest request,
        CancellationToken cancellationToken)
    {
        var user = new global::Keycloak.Net.Models.Users.User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.DisplayName,
            Enabled = true,
        };
        var created = await client.CreateUserAsync(options.Value.Realm, user, cancellationToken);
        return new CreateUserCredentialsResponse(created);
    }
}

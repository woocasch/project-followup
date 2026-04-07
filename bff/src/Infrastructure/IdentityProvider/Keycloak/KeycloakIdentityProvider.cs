namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Application.IdentityProvider;

public sealed class KeycloakIdentityProvider(
    IOptions<KeycloakSettings> options,
    global::Keycloak.Net.KeycloakClient client,
    ILogger<KeycloakIdentityProvider> logger) : IIdentityProvider
{
    public async Task<CreateUserCredentialsResponse> CreateUserCredentials(
        CreateUserCredentialsRequest request,
        CancellationToken cancellationToken)
    {
        logger.CreateUserStarted(request.Email);
        var user = new global::Keycloak.Net.Models.Users.User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.DisplayName,
            Enabled = true,
            Attributes = new Dictionary<string, IEnumerable<string>>
            {
                ["projectfollowup-userid"] = [request.UserId.Value.ToString()]
            }
        };
        var created = await client.CreateUserAsync(options.Value.Realm, user, cancellationToken);
        if (!created)
        {
            logger.CreateUserCreationFailed(request.Email);
            return CreateUserCredentialsResponse.Failed();
        }

        logger.CreateUserSearchingForUser(request.Email);
        var usersFound = (await client.GetUsersAsync(
            options.Value.Realm,
            email: request.Email,
            cancellationToken: cancellationToken))
            .ToList();
        var createdUser = usersFound.SingleOrDefault();
        if (createdUser is null)
        {
            logger.CreateUserUserNotFound(request.Email);
            return CreateUserCredentialsResponse.Failed();
        }

        var userId = Guid.Parse(createdUser.Id);
        logger.CreateUserCompleted(request.Email, userId);
        return CreateUserCredentialsResponse.Succeeded(userId);
    }
}

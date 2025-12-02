namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

using ProjectFollowUp.BFF.Application.IdentityProvider;

public sealed class HttpIdentityProvider(
    IHttpClientFactory clientFactory,
    IOptions<KeycloakSettings> settings,
    IMemoryCache cache) : IIdentityProviderClient
{
    public async Task<CreateUserCredentialsResponse> CreateUserCredentials(CreateUserCredentialsRequest request, CancellationToken cancellationToken)
    {
        var token = await cache.GetOrCreateAsync("KeycloakToken", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            return await GetToken();
        });
        if (string.IsNullOrEmpty(token))
        {
            return new CreateUserCredentialsResponse(false);
        }

        return new CreateUserCredentialsResponse(true);
    }

    public async Task<string> GetToken()
    {
        var clientId = settings.Value.ClientId;
        var clientSecret = settings.Value.ClientSecret;
        var realm = settings.Value.Realm;
        var webClient = clientFactory.CreateClient("Keycloak");
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"realms/{realm}/protocol/openid-connect/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret }
            })
        };
        var responseMessage = await webClient.SendAsync(requestMessage);
        return await responseMessage.Content.ReadAsStringAsync();
    }
}

namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

public sealed class DefaultKeycloakClient(
    IHttpClientFactory clientFactory,
    IOptions<KeycloakSettings> settings) : IKeycloakClient
{
    public async Task<GetServiceTokenResponse?> GetServiceToken(
        GetServiceTokenRequest request,
        CancellationToken cancellationToken)
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
        var responseMessage = await webClient.SendAsync(requestMessage, cancellationToken);
        try
        {
            var output = await responseMessage.Content.ReadFromJsonAsync<CreateTokenOutput>(cancellationToken);
            if (output is null)
            {
                return null;
            }

            return new(output.TokenType, output.AccessToken);
        }
        catch
        {
            return null;
        }
    }

    public async Task<CreateCredentialsResponse> CreateCredentials(CreateCredentialsRequest request, CancellationToken cancellationToken)
    {
        var realm = settings.Value.Realm;
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"admin/realms/{realm}/users");
        var payload = new CreateUserInput
        {
            UserName = request.UserName,
            Email = request.Email,
            Enabled = true,
            EmailVerified = true,
            Credentials =
            [
                new CreateUserInput.CredentialsInput
                {
                    Type = "password",
                    Value = request.Password,
                    Temporary = false
                }
            ]
        };
        requestMessage.Content = JsonContent.Create(payload);
        requestMessage.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(request.TokenType, request.AccessToken);
        var webClient = clientFactory.CreateClient("Keycloak");
        var responseMessage = await webClient.SendAsync(requestMessage, cancellationToken);
        return new CreateCredentialsResponse(responseMessage.IsSuccessStatusCode);
    }
}

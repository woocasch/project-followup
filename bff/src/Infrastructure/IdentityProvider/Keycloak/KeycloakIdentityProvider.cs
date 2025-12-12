namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

using ProjectFollowUp.BFF.Application.IdentityProvider;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

public sealed class KeycloakIdentityProvider(
    IKeycloakClient keycloakClient,
    IMemoryCache cache) : IIdentityProvider
{
    public async Task<CreateUserCredentialsResponse> CreateUserCredentials(
        CreateUserCredentialsRequest request,
        CancellationToken cancellationToken)
    {
        var token = await this.GetToken(cancellationToken);
        if (token is null)
        {
            return new CreateUserCredentialsResponse(false);
        }

        var createCredentialsRequest = new CreateCredentialsRequest(
            token.Value.TokenType,
            token.Value.AccessToken,
            request.Email,
            request.Email,
            "INITIAL_PASSWORD");
        var createCredentialsResponse = await keycloakClient.CreateCredentials(
            createCredentialsRequest,
            cancellationToken);
        return new CreateUserCredentialsResponse(createCredentialsResponse.Created);
    }

    private async Task<(string TokenType, string AccessToken)?> GetToken(CancellationToken cancellationToken)
    {
        return await cache.GetOrCreateAsync<(string TokenType, string AccessToken)?>("KeycloakToken", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
            var request = new GetServiceTokenRequest();
            var response = await keycloakClient.GetServiceToken(request, cancellationToken);
            if (response is null)
            {
                return null;
            }

            return (response.TokenType, response.AccessToken);
        });
    }
}

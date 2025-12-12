namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

public interface IKeycloakClient
{
    Task<GetServiceTokenResponse?> GetServiceToken(
        GetServiceTokenRequest request,
        CancellationToken cancellationToken);

    Task<CreateCredentialsResponse> CreateCredentials(
        CreateCredentialsRequest request,
        CancellationToken cancellationToken);
}

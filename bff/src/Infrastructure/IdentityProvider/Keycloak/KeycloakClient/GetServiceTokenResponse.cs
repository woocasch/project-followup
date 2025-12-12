namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient;

public sealed class GetServiceTokenResponse(
    string tokenType,
    string accessToken)
{
    public string TokenType { get; } = tokenType;

    public string AccessToken { get; } = accessToken;
}

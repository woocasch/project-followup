namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

public sealed class CreateCredentialsRequest(
    string tokenType,
    string accessToken,
    string username,
    string email,
    string password)
{
    public string TokenType { get; } = tokenType;

    public string AccessToken { get; } = accessToken;

    public string UserName { get; } = username;

    public string Email { get; } = email;

    public string Password { get; } = password;
}

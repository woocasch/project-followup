namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

using System.Text.Json.Serialization;

internal sealed class CreateTokenOutput()
{
    [JsonPropertyName("token_type")]
    public required string TokenType { get; init; }

    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("expires_in")]
    public required int ExpiresIn { get; init; }

    [JsonPropertyName("refresh_expires_in")]
    public required int RefreshExpiresIn { get; init; }

    [JsonPropertyName("not-before-policy")]
    public required int NotBeforePolicy { get; init; }

    [JsonPropertyName("scope")]
    public required string Scope { get; init; }
}

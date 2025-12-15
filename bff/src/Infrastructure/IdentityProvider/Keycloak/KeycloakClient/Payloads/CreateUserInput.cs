namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

using System.Text.Json.Serialization;

public sealed class CreateUserInput
{
    [JsonPropertyName("username")]
    public required string UserName { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("enabled")]
    public required bool Enabled { get; init; }

    [JsonPropertyName("emailVerified")]
    public required bool EmailVerified { get; init; }

    [JsonPropertyName("credentials")]
    public required CredentialsInput[] Credentials { get; init; }

    public sealed class CredentialsInput
    {
        [JsonPropertyName("type")]
        public required string Type { get; init; }

        [JsonPropertyName("value")]
        public required string Value { get; init; }

        [JsonPropertyName("temporary")]
        public required bool Temporary
        {
            get; init;
        }
    }
}

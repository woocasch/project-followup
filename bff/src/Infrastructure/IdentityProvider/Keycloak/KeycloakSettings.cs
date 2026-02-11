namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

public sealed class KeycloakSettings
{
    public required string Realm { get; init; }

    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required string BaseAddress { get; init; }

    public required string ValidIssuer { get; init; }
}

namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak.KeycloakClient.Payloads;

public sealed class CreateCredentialsResponse(
    bool created)
{
    public bool Created { get; } = created;
}

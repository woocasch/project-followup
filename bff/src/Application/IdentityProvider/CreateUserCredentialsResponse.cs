namespace ProjectFollowUp.BFF.Application.IdentityProvider;

public sealed class CreateUserCredentialsResponse(
    bool created)
{
    public bool Created { get; } = created;
}

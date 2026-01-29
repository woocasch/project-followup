namespace ProjectFollowUp.BFF.Application.IdentityProvider;

public sealed class CreateUserCredentialsResponse
{
    private readonly Guid? credentialsId;

    private CreateUserCredentialsResponse(
        Guid? credentialsId,
        bool created)
    {
        this.credentialsId = credentialsId;
        this.CredentialsCreated = created;
    }

    public bool CredentialsCreated { get; }

    public Guid CredentialsId
    {
        get
        {
            if (this.credentialsId is null)
            {
                throw new InvalidOperationException(IdentityProviderResources.CreateUserCredentialsResponse_CredentialsNotCreated);
            }

            return this.credentialsId.Value;
        }
    }

    public static CreateUserCredentialsResponse Failed() => new(null, false);

    public static CreateUserCredentialsResponse Succeeded(Guid credentialsId) => new(credentialsId, true);
}

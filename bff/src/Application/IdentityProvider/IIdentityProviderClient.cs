namespace ProjectFollowUp.BFF.Application.IdentityProvider;

public interface IIdentityProviderClient
{
    Task<CreateUserCredentialsResponse> CreateUserCredentials(
        CreateUserCredentialsRequest request,
        CancellationToken cancellationToken);
}

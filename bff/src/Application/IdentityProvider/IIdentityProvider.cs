namespace ProjectFollowUp.BFF.Application.IdentityProvider;

public interface IIdentityProvider
{
    Task<CreateUserCredentialsResponse> CreateUserCredentials(
        CreateUserCredentialsRequest request,
        CancellationToken cancellationToken);
}

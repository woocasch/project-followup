namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.IdentityProvider;

public sealed class HttpIdentityProvider : IIdentityProviderClient
{
    public Task<CreateUserCredentialsResponse> CreateUserCredentials(CreateUserCredentialsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

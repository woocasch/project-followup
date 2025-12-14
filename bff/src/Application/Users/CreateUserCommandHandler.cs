namespace ProjectFollowUp.BFF.Application.Users;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.IdentityProvider;
using ProjectFollowUp.BFF.Domain.Users;
using ProjectFollowUp.BFF.Domain.Users.UserEvents;

public sealed class CreateUserCommandHandler(
    IIdentityProvider identityProvider) : CommandHandlerBase<CreateUserCommand>
{
    protected override async Task<CommandResult> HandleCommand(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var credentialsCreated = await CreateUserCredentials(
            command,
            cancellationToken);
        if (!credentialsCreated)
        {
            return CommandResult.Failure("CreateUserCredentialsFailed");
        }

        await CreateUserProfile(command, cancellationToken);
        return CommandResult.Success();
    }

    private async Task CreateUserProfile(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        await Task.Yield();
        var email = EmailAddress.FromString(command.Email);
        var userCreatedEvent = new UserCreated(
            command.Id,
            command.DisplayName,
            email,
            DateTimeOffset.UtcNow);
        UsersStore.AddEvent(command.Id, userCreatedEvent);
    }

    private async Task<bool> CreateUserCredentials(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var request = new CreateUserCredentialsRequest(
            command.Id,
            command.Email,
            command.DisplayName);
        var response = await identityProvider.CreateUserCredentials(
            request,
            cancellationToken);
        return response.Created;
    }
}

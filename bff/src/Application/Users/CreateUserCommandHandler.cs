namespace ProjectFollowUp.BFF.Application.Users;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.IdentityProvider;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Domain.User.Events;

public sealed class CreateUserCommandHandler(
    IIdentityProvider identityProvider,
    IEventStreamsRepository eventsRepository) : CommandHandlerBase<CreateUserCommand>
{
    protected override async Task<CommandResult> HandleCommand(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var credentialsId = await CreateUserCredentials(
            command,
            cancellationToken);
        if (!credentialsId.HasValue)
        {
            return CommandResult.Failure("CreateUserCredentialsFailed");
        }

        await CreateUserProfile(command, credentialsId.Value, cancellationToken);
        return CommandResult.Success();
    }

    private async Task CreateUserProfile(
        CreateUserCommand command,
        Guid credentialsId,
        CancellationToken cancellationToken)
    {
        await Task.Yield();
        var email = EmailAddress.FromString(command.Email);
        var user = UserAggregateRoot.Create(
            command.Id,
            credentialsId,
            command.DisplayName,
            email,
            DateTimeOffset.UtcNow);
        await eventsRepository.StoreStreamAsync(
            user,
            cancellationToken);
    }

    private async Task<Guid?> CreateUserCredentials(
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
        if (!response.CredentialsCreated)
        {
            return null;
        }

        return response.CredentialsId;
    }
}

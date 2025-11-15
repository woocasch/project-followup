namespace ProjectFollowUp.BFF.Application.Cqrs;

public interface IMediator
{
    Task<CommandResult> Send(ICommand command, CancellationToken cancellationToken);
}

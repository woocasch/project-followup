namespace ProjectFollowUp.BFF.Application.Cqrs;

public interface ICommandHandler
{
    Task<CommandResult> Handle(ICommand command, CancellationToken cancellationToken);
}

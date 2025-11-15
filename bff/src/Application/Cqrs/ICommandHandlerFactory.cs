namespace ProjectFollowUp.BFF.Application.Cqrs;

public interface ICommandHandlerFactory
{
    ICommandHandler? CreateHandler(ICommand command);
}

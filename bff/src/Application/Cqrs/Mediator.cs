namespace ProjectFollowUp.BFF.Application.Cqrs;

public sealed class Mediator : IMediator
{
    private readonly ICommandHandlerFactory commandHandlerFactory;

    public Mediator(ICommandHandlerFactory commandHandlerFactory)
    {
        this.commandHandlerFactory = commandHandlerFactory;
    }

    public async Task<CommandResult> Send(ICommand command, CancellationToken cancellationToken)
    {
        var commandHandler = this.commandHandlerFactory.CreateHandler(command);
        if (commandHandler is null)
        {
            var message = $"No handler found for command of type {command.GetType().FullName}.";
            throw new InvalidOperationException(message);
        }

        return await commandHandler.Handle(command, cancellationToken);
    }
}

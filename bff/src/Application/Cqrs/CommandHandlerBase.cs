namespace ProjectFollowUp.BFF.Application.Cqrs;

public abstract class CommandHandlerBase<TCommand> : ICommandHandler
    where TCommand : ICommand
{
    public async Task<CommandResult> Handle(ICommand command, CancellationToken cancellationToken)
    {
        if (command is not TCommand typedCommand)
        {
            var message = $"Invalid command type. Expected: {typeof(TCommand).FullName}, Actual: {command.GetType().FullName}";
            throw new InvalidOperationException(message);
        }

        try
        {
            return await this.HandleCommand(typedCommand, cancellationToken);
        }
        catch (Exception ex)
        {
            return CreateFatalError(ex);
        }
    }

    protected abstract Task<CommandResult> HandleCommand(TCommand command, CancellationToken cancellationToken);

    private static CommandResult CreateFatalError(Exception ex)
    {
        return CommandResult.FatalError(ex);
    }
}

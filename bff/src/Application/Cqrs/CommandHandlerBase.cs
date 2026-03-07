namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

public abstract class CommandHandlerBase<TCommand>(
    ILogger<CommandHandlerBase<TCommand>> logger) : ICommandHandler
    where TCommand : ICommand
{
    public async Task<CommandResult> Handle(ICommand command, CancellationToken cancellationToken)
    {
        logger.HandleStarted(typeof(TCommand));
        if (command is null)
        {
            logger.NullCommand();
            throw new ArgumentNullException(nameof(command));
        }

        if (command is not TCommand typedCommand)
        {
            logger.InvalidCommandType(typeof(TCommand), command.GetType());
            var message = $"Invalid command type. Expected: {typeof(TCommand).FullName}, Actual: {command.GetType().FullName}";
            throw new InvalidOperationException(message);
        }

        try
        {
            logger.PassingToStronglyTypedHandle(typedCommand.GetType());
            var result = await this.HandleCommand(typedCommand, cancellationToken);
            logger.Completed(typedCommand.GetType());
            return result;
        }
        catch (Exception ex)
        {
            logger.ExceptionOccurred(typedCommand.GetType(), ex);
            return CommandResult.Failure("ExceptionOccurred", ex);
        }
    }

    protected abstract Task<CommandResult> HandleCommand(TCommand command, CancellationToken cancellationToken);
}

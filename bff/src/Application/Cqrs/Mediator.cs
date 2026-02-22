namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

public sealed class Mediator(
    IHandlerFactory commandHandlerFactory,
    ILogger<Mediator> logger) : IMediator
{
    public async Task<CommandResult> Send(ICommand command, CancellationToken cancellationToken)
    {
        logger.SendStarted(command.GetType());
        var commandHandler = commandHandlerFactory.CreateCommandHandler(command);
        if (commandHandler is null)
        {
            logger.CommandHandlerNotFound(command.GetType());
            var message = $"No handler found for command of type {command.GetType().FullName}.";
            throw new InvalidOperationException(message);
        }

        logger.CallingCommandHandler(commandHandler.GetType(), command.GetType());
        try
        {
            var result = await commandHandler.Handle(command, cancellationToken);
            logger.CommandHandlerReturnedResult(commandHandler.GetType(), command.GetType(), result.IsSuccess);
            return result;
        }
        catch (Exception ex)
        {
            logger.CommandHandlerThrownException(commandHandler.GetType(), command.GetType(), ex);
            var message = $"An error occurred while handling command of type {command.GetType().FullName}.";
            throw new InvalidOperationException(message, ex);
        }
    }

    public async Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : notnull
    {
        logger.FetchStarted(query.GetType());
        var queryHandler = commandHandlerFactory.CreateQueryHandler<TResult>(query);
        if (queryHandler is null)
        {
            logger.QueryHandlerNotFound(query.GetType());
            var message = $"No handler found for query of type {query.GetType().FullName}.";
            throw new InvalidOperationException(message);
        }

        try
        {
            logger.CallingQueryHandler(queryHandler.GetType(), query.GetType());
            var result = await queryHandler.Handle(query, cancellationToken);
            logger.QueryHandlerReturnedResult(queryHandler.GetType(), query.GetType());
            return result;
        }
        catch (Exception ex)
        {
            logger.QueryHandlerThrownException(queryHandler.GetType(), query.GetType(), ex);
            var message = $"An error occurred while handling query of type {query.GetType().FullName}.";
            throw new InvalidOperationException(message, ex);
        }
    }
}

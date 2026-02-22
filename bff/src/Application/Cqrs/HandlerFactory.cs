namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class HandlerFactory(
    IServiceProvider serviceProvider,
    ILogger<HandlerFactory> logger) : IHandlerFactory
{
    public ICommandHandler? CreateCommandHandler(ICommand command)
    {
        logger.CreateCommandHandlerStarted(command.GetType());
        var commandHandlerName = NamingConventions.CommandHandlerName(command);
        logger.QueryHandlerNameFound(command.GetType(), commandHandlerName);
        var result = serviceProvider.GetKeyedService<ICommandHandler>(commandHandlerName);
        if (result is null)
        {
            logger.CommandHandlerNotFound(command.GetType(), commandHandlerName);
        }
        else
        {
            logger.CommandHandlerFound(command.GetType(), commandHandlerName);
        }

        return result;
    }

    public IQueryHandler<TResult>? CreateQueryHandler<TResult>(IQuery<TResult> query)
        where TResult : notnull
    {
        logger.CreateQueryHandlerStarted(query.GetType());
        var queryHandlerName = NamingConventions.QueryHandlerName(query);
        logger.QueryHandlerNameFound(query.GetType(), queryHandlerName);
        var result = serviceProvider.GetKeyedService<IQueryHandler<TResult>>(queryHandlerName);
        if (result is null)
        {
            logger.QueryHandlerNotFound(query.GetType(), queryHandlerName);
        }
        else
        {
            logger.QueryHandlerFound(query.GetType(), queryHandlerName);
        }

        return result;
    }
}

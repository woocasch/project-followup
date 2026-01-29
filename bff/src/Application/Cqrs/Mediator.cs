namespace ProjectFollowUp.BFF.Application.Cqrs;

public sealed class Mediator(IHandlerFactory commandHandlerFactory) : IMediator
{
    private readonly IHandlerFactory commandHandlerFactory = commandHandlerFactory;

    public async Task<CommandResult> Send(ICommand command, CancellationToken cancellationToken)
    {
        var commandHandler = this.commandHandlerFactory.CreateCommandHandler(command);
        if (commandHandler is null)
        {
            var message = $"No handler found for command of type {command.GetType().FullName}.";
            throw new InvalidOperationException(message);
        }

        return await commandHandler.Handle(command, cancellationToken);
    }

    public async Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : notnull
    {
        var queryHandler = this.commandHandlerFactory.CreateQueryHandler<TResult>(query);
        if (queryHandler is null)
        {
            var message = $"No handler found for query of type {query.GetType().FullName}.";
            throw new InvalidOperationException(message);
        }

        var result = await queryHandler.Handle(query, cancellationToken);
        if (result is null)
        {
            var message = $"Query handler returned null for query of type {query.GetType().FullName}.";
            throw new InvalidOperationException(message);
        }

        return result;
    }
}

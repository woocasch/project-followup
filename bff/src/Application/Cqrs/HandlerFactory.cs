namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.DependencyInjection;

public sealed class HandlerFactory : IHandlerFactory
{
    private readonly IServiceProvider serviceProvider;

    public HandlerFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ICommandHandler? CreateCommandHandler(ICommand command)
    {
        var commandHandlerName = NamingConventions.CommandHandlerName(command);
        return this.serviceProvider.GetKeyedService<ICommandHandler>(commandHandlerName);
    }

    public IQueryHandler<TResult>? CreateQueryHandler<TResult>(IQuery<TResult> query)
        where TResult : notnull
    {
        var queryHandlerName = NamingConventions.QueryHandlerName(query);
        return this.serviceProvider.GetKeyedService<IQueryHandler<TResult>>(queryHandlerName);
    }
}

namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.DependencyInjection;

public sealed class CommandHandlerFactory : ICommandHandlerFactory
{
    private readonly IServiceProvider serviceProvider;

    public CommandHandlerFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ICommandHandler? CreateHandler(ICommand command)
    {
        var commandHandlerName = NamingConventions.CommandHandlerName(command);
        return this.serviceProvider.GetKeyedService<ICommandHandler>(commandHandlerName);
    }
}

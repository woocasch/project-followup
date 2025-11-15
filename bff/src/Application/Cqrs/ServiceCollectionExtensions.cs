namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCqrs(
        this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
        return services;
    }

    public static IServiceCollection RegisterCommandHandler<TCommand, THandler>(
        this IServiceCollection services)
        where TCommand : Cqrs.ICommand
        where THandler : class, Cqrs.ICommandHandler
    {
        var key = NamingConventions.CommandHandlerName<TCommand>();
        services.AddKeyedTransient<Cqrs.ICommandHandler, THandler>(key);
        return services;
    }
}

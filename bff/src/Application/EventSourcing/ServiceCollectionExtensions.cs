namespace ProjectFollowUp.BFF.Application.EventSourcing;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventSourcing(
        this IServiceCollection services)
    {
        services
            .AddSingleton<IProjectionWorkerFactory, ProjectionWorkerFactory>();
        return services;
    }
}

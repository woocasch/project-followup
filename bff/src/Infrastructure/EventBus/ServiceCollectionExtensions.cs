namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.EventsBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();
        return services;
    }
}

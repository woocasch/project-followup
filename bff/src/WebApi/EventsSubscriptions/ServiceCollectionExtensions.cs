namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Infrastructure.EventBus;

using RabbitMQ.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureEventsSubscriptions(
        this IServiceCollection services)
    {
        services.AddSingleton<IConnectionFactory, ConnectionFactory>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<EventBusSettings>>().Value;
            return new ConnectionFactory
            {
                Uri = new Uri(settings.ConnectionString),
            };
        });
        services.AddEventBus();
        services.AddHostedService<SubscriptionsManager>();
        services.AddHostedService<ModelHydration>();
        return services;
    }
}

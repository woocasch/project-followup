namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.EventsBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEventBus(
        this IServiceCollection services)
    {
        services
            .AddScoped<IEventPublisher, EventPublisher>()
            .AddSingleton<INamesMappings, NamesMappings>()
            .AddTransient<IQueueBinder, QueueBinder>()
            .RegisterConsumer<ActivationLinks.CreateActivationLinkConsumer>()
            .RegisterConsumer<Documents.SendActivationMailConsumer>();
        return services;
    }

    private static IServiceCollection RegisterConsumer<T>(
        this IServiceCollection serviceCollection)
        where T : class, IConsumer<T>
    {
        serviceCollection.AddTransient<IConsumer, T>(sp =>
        {
            var queueBinder = sp.GetRequiredService<IQueueBinder>();
            var createConsumerTask = queueBinder.CreateConsumer<T>(CancellationToken.None);
            createConsumerTask.Wait();
            return createConsumerTask.Result;
        });
        return serviceCollection;
    }
}

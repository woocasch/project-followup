namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Threading;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceProviderExtensions
{
    public static async Task<IEnumerable<IConsumer>> BindAllConsumers(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var consumers = serviceProvider
            .GetServices<IConsumer>()
            .ToList();
        var namesMapping = serviceProvider.GetRequiredService<INamesMappings>();
        foreach (var consumer in consumers)
        {
            var queueName = namesMapping.GetQueueNameForConsumer(consumer);
            await consumer.BindToQueue(queueName, cancellationToken);
        }

        return consumers;
    }
}

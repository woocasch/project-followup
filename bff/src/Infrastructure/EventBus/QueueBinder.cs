namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using RabbitMQ.Client;

public sealed class QueueBinder(
    IServiceProvider serviceProvider) : IQueueBinder
{
    public async Task<T> CreateConsumer<T>(CancellationToken cancellationToken)
        where T : IConsumer, IConsumer<T>
    {
        var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
        var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        var consumer = T.Create(serviceProvider, channel);
        return consumer;
    }
}

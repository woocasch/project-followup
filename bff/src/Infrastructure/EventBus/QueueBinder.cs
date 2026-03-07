namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

public sealed class QueueBinder(
    IServiceProvider serviceProvider,
    ILogger<QueueBinder> logger) : IQueueBinder
{
    public async Task<T> CreateConsumer<T>(CancellationToken cancellationToken)
        where T : IConsumer, IConsumer<T>
    {
        logger.Started(typeof(T));
        var connectionFactory = serviceProvider.GetRequiredService<IConnectionFactory>();
        var connection = await connectionFactory.CreateConnectionAsync(cancellationToken);
        logger.ConnectionCreated(typeof(T));
        var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        logger.ChannelCreated(typeof(T));
        var consumer = T.Create(serviceProvider, channel);
        logger.Completed(typeof(T));
        return consumer;
    }
}

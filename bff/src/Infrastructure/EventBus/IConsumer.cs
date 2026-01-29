namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using RabbitMQ.Client;

public interface IConsumer
{
    Task BindToQueue(string queueName, CancellationToken cancellationToken);

    void UnbindFromQueue();
}

public interface IConsumer<T> : IConsumer
    where T : IConsumer<T>
{
    static abstract T Create(IServiceProvider serviceProvider, IChannel channel);
}

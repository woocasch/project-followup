namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

public interface IQueueBinder
{
    Task<T> CreateConsumer<T>(CancellationToken cancellationToken)
        where T : IConsumer, IConsumer<T>;
}

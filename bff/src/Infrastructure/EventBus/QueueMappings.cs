namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

public sealed class QueueMappings
{
    public required List<Exchange> Exchanges { get; init; }

    public required List<Queue> Queues { get; init; }

    public sealed class Exchange
    {
        public required string EventType { get; init; }

        public required string ExchangeName { get; init; }
    }

    public sealed class Queue
    {
        public required string HandlerType { get; init; }

        public required string QueueName { get; init; }
    }
}

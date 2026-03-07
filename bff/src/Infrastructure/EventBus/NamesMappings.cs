namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public sealed class NamesMappings(
    IOptions<QueueMappings> mappingsOptions,
    ILogger<NamesMappings> logger)
    : INamesMappings
{
    private readonly QueueMappings mappings = mappingsOptions.Value;

    public string GetExchangeNameForEvent(object @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        logger.GetExchangeNameStarted(@event.GetType());
        var eventType = GetTypeName(@event);
        var mapping = mappings.Exchanges
            .SingleOrDefault(m => m.EventType == eventType);
        if (mapping is null)
        {
            logger.NoExchangeMappingFound(@event.GetType());
            throw new InvalidOperationException($"No exchange mapping found for event type '{eventType}'.");
        }

        logger.GetExchangeNameCompleted(@event.GetType(), mapping.ExchangeName);
        return mapping.ExchangeName;
    }

    public string GetQueueNameForConsumer(IConsumer consumer)
    {
        ArgumentNullException.ThrowIfNull(consumer);
        logger.GetQueueNameStarted(consumer.GetType());
        var consumerType = GetTypeName(consumer);
        var mapping = mappings.Queues
            .SingleOrDefault(q => q.HandlerType == consumerType);
        if (mapping is null)
        {
            logger.NoQueueMappingFound(consumer.GetType());
            throw new InvalidOperationException($"No queue mapping found for consumer type '{consumerType}'.");
        }

        logger.GetQueueNameCompleted(consumer.GetType(), mapping.QueueName);
        return mapping.QueueName;
    }

    private static string GetTypeName(object value)
    {
        return GetTypeName(value.GetType());
    }

    private static string GetTypeName(Type type)
    {
        var typeName = type.FullName;
        var assemblyName = type.Assembly.GetName().Name;
        return $"{typeName}, {assemblyName}";
    }
}

namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using Microsoft.Extensions.Options;

public sealed class NamesMappings(
    IOptions<QueueMappings> mappingsOptions) : INamesMappings
{
    private readonly QueueMappings mappings = mappingsOptions.Value;

    public string GetExchangeNameForEvent(object @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        var eventType = GetTypeName(@event);
        var mapping = mappings.Exchanges
            .SingleOrDefault(m => m.EventType == eventType);
        if (mapping is null)
        {
            throw new InvalidOperationException($"No exchange mapping found for event type '{eventType}'.");
        }

        return mapping.ExchangeName;
    }

    public string GetQueueNameForConsumer(IConsumer consumer)
    {
        var consumerType = GetTypeName(consumer);
        var mapping = mappings.Queues
            .SingleOrDefault(q => q.HandlerType == consumerType);
        if (mapping is null)
        {
            throw new InvalidOperationException($"No queue mapping found for consumer type '{consumerType}'.");
        }

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

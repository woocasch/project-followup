namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

public interface INamesMappings
{
    string GetExchangeNameForEvent(object @event);

    string GetQueueNameForConsumer(IConsumer consumer);
}

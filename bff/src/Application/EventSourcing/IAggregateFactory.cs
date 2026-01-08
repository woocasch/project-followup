namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IAggregateFactory
{
    TAggregate Create<TAggregate>(
        IEnumerable<EventEnvelope> events,
        Func<IEnumerable<object>, TAggregate> rehydrationFunction)
        where TAggregate : class;
}

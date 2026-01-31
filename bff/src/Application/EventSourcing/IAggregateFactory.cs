namespace ProjectFollowUp.BFF.Application.EventSourcing;

using ProjectFollowUp.BFF.Domain;

public interface IAggregateFactory
{
    TAggregate Create<TAggregate>(
        IEnumerable<EventEnvelope> events,
        Func<IEnumerable<IEvent>, TAggregate> rehydrationFunction)
        where TAggregate : class;
}

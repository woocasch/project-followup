namespace ProjectFollowUp.BFF.Application.EventSourcing;

using ProjectFollowUp.BFF.Domain;

public interface IAggregateFactory
{
    TAggregate Create<TAggregate>(
        IEnumerable<EventEnvelope> events,
        Func<IEnumerable<IAggregateEvent>, TAggregate> rehydrationFunction)
        where TAggregate : class;
}

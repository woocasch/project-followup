namespace ProjectFollowUp.BFF.Application.EventSourcing;

using System;
using System.Collections.Generic;

using ProjectFollowUp.BFF.Domain;

public sealed class AggregateFactory : IAggregateFactory
{
    public TAggregate Create<TAggregate>(
        IEnumerable<EventEnvelope> events,
        Func<IEnumerable<IEvent>, TAggregate> rehydrationFunction)
        where TAggregate : class
    {
        var domainEvents = events.Select(e => e.Event);
        return rehydrationFunction(domainEvents);
    }
}

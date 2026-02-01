namespace ProjectFollowUp.BFF.Domain;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public abstract class AggregateRootBase<TId> : IAggregateRoot
    where TId : notnull
{
    private readonly Collection<IAggregateEvent> uncommittedEvents = [];

    public TId Id { get; protected set; } = default!;

    public abstract Guid AggregateId { get; }

    public virtual string AggregateType => this.GetType().FullName!;

    public IEnumerable<IAggregateEvent> GetUncommittedEvents()
    {
        return this.uncommittedEvents.ToList().AsReadOnly();
    }

    protected void RecreateFromHistory(IEnumerable<IAggregateEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            this.When(domainEvent);
        }
    }

    protected void Apply(IAggregateEvent domainEvent)
    {
        this.uncommittedEvents.Add(domainEvent);
        this.When(domainEvent);
    }

    protected abstract void When(IAggregateEvent domainEvent);
}

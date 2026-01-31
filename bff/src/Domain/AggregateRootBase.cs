namespace ProjectFollowUp.BFF.Domain;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public abstract class AggregateRootBase<TId> : IAggregateRoot
    where TId : notnull
{
    private readonly Collection<IAggregateEvent> commitedEvents = [];

    private readonly Collection<IAggregateEvent> uncommitedEvents = [];

    public TId Id { get; protected set; } = default!;

    public abstract Guid AggregateId { get; }

    public virtual string AggregateType => this.GetType().FullName!;

    public IEnumerable<IAggregateEvent> GetUncommitedEvents()
    {
        return this.uncommitedEvents.ToList().AsReadOnly();
    }

    protected void RecreateFromHistory(IEnumerable<IAggregateEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            this.commitedEvents.Add(domainEvent);
            this.When(domainEvent);
        }
    }

    protected void Apply(IAggregateEvent domainEvent)
    {
        this.uncommitedEvents.Add(domainEvent);
        this.When(domainEvent);
    }

    protected abstract void When(IAggregateEvent domainEvent);
}

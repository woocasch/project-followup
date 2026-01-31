namespace ProjectFollowUp.BFF.Domain;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public abstract class AggregateRootBase<TId> : IAggregateRoot
    where TId : notnull
{
    private readonly Collection<IEvent> commitedEvents = [];

    private readonly Collection<IEvent> uncommitedEvents = [];

    public TId Id { get; protected set; } = default!;

    public abstract Guid AggregateId { get; }

    public virtual string AggregateType => this.GetType().FullName!;

    public IEnumerable<IEvent> GetUncommitedEvents()
    {
        return this.uncommitedEvents.ToList().AsReadOnly();
    }

    protected void RecreateFromHistory(IEnumerable<IEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            this.commitedEvents.Add(domainEvent);
            this.When(domainEvent);
        }
    }

    protected void Apply(IEvent domainEvent)
    {
        this.uncommitedEvents.Add(domainEvent);
        this.When(domainEvent);
    }

    protected abstract void When(IEvent domainEvent);
}

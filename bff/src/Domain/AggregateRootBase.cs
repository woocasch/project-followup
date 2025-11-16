namespace ProjectFollowUp.BFF.Domain;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public abstract class AggregateRootBase<TId> : IAggregateRoot
    where TId : notnull
{
    private readonly Collection<object> uncommitedDomainEvents = [];

    public TId Id { get; protected set; } = default!;

    public Guid AggregateId
    {
        get
        {
            if (this.Id is Guid guidId)
            {
                return guidId;
            }

            return Guid.Empty;
        }
    }

    public virtual string AggregateType => this.GetType().FullName!;

    public IEnumerable<object> GetUncommitedDomainEvents()
    {
        return this.uncommitedDomainEvents.ToList().AsReadOnly();
    }

    protected void RecreateFromHistory(IEnumerable<object> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            this.When(domainEvent);
        }
    }

    protected void Apply(object domainEvent)
    {
        this.uncommitedDomainEvents.Add(domainEvent);
        this.When(domainEvent);
    }

    protected abstract void When(object domainEvent);
}

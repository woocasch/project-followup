namespace ProjectFollowUp.BFF.Domain;

public interface IAggregateRoot
{
    Guid AggregateId { get; }

    string AggregateType { get; }

    IEnumerable<IEvent> GetUncommitedEvents();
}

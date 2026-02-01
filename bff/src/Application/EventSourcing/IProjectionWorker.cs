namespace ProjectFollowUp.BFF.Application.EventSourcing;

using ProjectFollowUp.BFF.Domain;

public interface IProjectionWorker
{
    Task Materialize(IAggregateEvent aggregateEvent, CancellationToken cancellationToken);
}

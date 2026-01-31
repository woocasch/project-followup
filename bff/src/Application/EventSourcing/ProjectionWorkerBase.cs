namespace ProjectFollowUp.BFF.Application.EventSourcing;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Domain;

public abstract class ProjectionWorkerBase<TAggregateEvent> : IProjectionWorker
    where TAggregateEvent : IAggregateEvent
{
    public async Task Materialize(IAggregateEvent aggregateEvent, CancellationToken cancellationToken)
    {
        if (aggregateEvent is not TAggregateEvent cast)
        {
            return;
        }

        await this.Materialize(cast, cancellationToken);
    }

    protected abstract Task Materialize(TAggregateEvent domainEvent, CancellationToken cancellationToken);
}

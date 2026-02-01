namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IProjectionWorkerFactory
{
    IEnumerable<IProjectionWorker> Create(Type aggregateEventType);
}

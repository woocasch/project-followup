namespace ProjectFollowUp.BFF.Application.EventSourcing;

using ProjectFollowUp.BFF.Domain;

public static class EventStreamsRepositoryExtensions
{
    public static async Task StoreStreamAsync<TAggregate>(
        this IEventStreamsRepository repository,
        TAggregate aggregate,
        CancellationToken cancellationToken)
        where TAggregate : class, IAggregateRoot
    {
        var eventsToStore = aggregate.GetUncommitedEvents();
        var id = aggregate.AggregateId;
        await repository.AppendToStreamAsync<TAggregate>(
            id,
            eventsToStore,
            expectedVersion: await repository.GetStreamVersionAsync<TAggregate>(id, cancellationToken),
            cancellationToken);
    }
}

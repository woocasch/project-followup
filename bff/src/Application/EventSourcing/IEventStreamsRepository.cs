namespace ProjectFollowUp.BFF.Application.EventSourcing;

using ProjectFollowUp.BFF.Domain;

public interface IEventStreamsRepository
{
    Task AppendToStreamAsync<TAggregate>(
        Guid aggregateId,
        IEnumerable<IEvent> events,
        ulong expectedVersion,
        CancellationToken cancellationToken)
        where TAggregate : class;

    Task<IEnumerable<EventEnvelope>> ReadStreamAsync<TAggregate>(
        Guid aggregateId,
        CancellationToken cancellationToken)
        where TAggregate : class;

    Task<ulong> GetStreamVersionAsync<TAggregate>(
        Guid aggregateId,
        CancellationToken cancellationToken)
        where TAggregate : class;

    Task<bool> StreamExistsAsync<TAggregate>(
        Guid aggregateId,
        CancellationToken cancellationToken)
        where TAggregate : class;

    Task DeleteStreamAsync<TAggregate>(
        Guid aggregateId,
        CancellationToken cancellationToken)
        where TAggregate : class;
}

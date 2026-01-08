namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IEventStreamsRepository
{
    Task AppendToStreamAsync<TAggregate>(
        Guid aggregateId,
        IEnumerable<object> events,
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

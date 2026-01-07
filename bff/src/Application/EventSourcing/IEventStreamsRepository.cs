namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IEventStreamsRepository
{
    Task AppendToStreamAsync<TAggregate>(
        string aggregateId,
        IEnumerable<object> events,
        ulong expectedVersion,
        CancellationToken cancellationToken)
        where TAggregate : class;

    Task<IEnumerable<EventEnvelope>> ReadStreamAsync(
        string aggregateId,
        CancellationToken cancellationToken);

    Task<ulong> GetStreamVersionAsync(
        string aggregateId,
        CancellationToken cancellationToken);

    Task<bool> StreamExistsAsync(
        string aggregateId,
        CancellationToken cancellationToken);

    Task DeleteStreamAsync(
        string aggregateId,
        CancellationToken cancellationToken);
}

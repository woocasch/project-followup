namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IEventStreamsRepository
{
    Task AppendToStreamAsync(
        string streamId,
        IEnumerable<object> events,
        ulong expectedVersion,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<EventEnvelope>> ReadStreamAsync(
        string streamId,
        CancellationToken cancellationToken = default);

    Task<ulong> GetStreamVersionAsync(
        string streamId,
        CancellationToken cancellationToken = default);

    Task<bool> StreamExistsAsync(
        string streamId,
        CancellationToken cancellationToken = default);

    Task DeleteStreamAsync(
        string streamId,
        CancellationToken cancellationToken = default);
}

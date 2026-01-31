namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IProjectionWriter<TRecord, TId>
    where TRecord : struct
{
    Task<TRecord?> Get(TId id, CancellationToken cancellationToken);

    Task Insert(TRecord record, CancellationToken cancellationToken);

    Task Update(TRecord record, CancellationToken cancellationToken);
}

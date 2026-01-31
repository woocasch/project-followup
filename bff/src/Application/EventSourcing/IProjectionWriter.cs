namespace ProjectFollowUp.BFF.Application.EventSourcing;

public interface IProjectionWriter<TRecord, TId>
    where TRecord : struct
{
    Task<TRecord?> Get(TId id, CancellationToken cancellationToken);

    Task Upsert(TRecord record, CancellationToken cancellationToken);
}

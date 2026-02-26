namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.EventSourcing;

public interface IProjectionWriterBase
{
}

public abstract class ProjectionWriterBase<TRecord, TDto, TId>(
    ILogger<ProjectionWriterBase<TRecord, TDto, TId>> logger)
    : IProjectionWriter<TRecord, TId>, IProjectionWriterBase
    where TRecord : struct
{
    public virtual async Task<TRecord?> Get(TId id, CancellationToken cancellationToken)
    {
        logger.GetStarted();
        var collection = this.GetCollection();
        var filterBuilder = Builders<TDto>.Filter;
        var filter = this.ApplyFilterById(filterBuilder, id);
        var searchOptions = new FindOptions<TDto>()
        {
            Limit = 1,
        };
        logger.GetSendingQuery();
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            logger.GetNoItemsFound();
            return null;
        }

        var found = items[0];
        logger.GetCompleted();
        return this.MapFromDto(found);
    }

    public virtual async Task Insert(TRecord record, CancellationToken cancellationToken)
    {
        logger.InsertStarted();
        var dto = this.MapFromRecord(record);
        var collection = this.GetCollection();
        await collection.InsertOneAsync(
                dto,
                null,
                cancellationToken);
        logger.InsertCompleted();
    }

    public virtual async Task Update(TRecord record, CancellationToken cancellationToken)
    {
        logger.UpdateStarted();
        var filterBuilder = Builders<TDto>.Filter;
        var filter = this.ApplyFilterById(filterBuilder, this.GetIdFromRecord(record));
        var update = this.ApplyUpdateDefinition(Builders<TDto>.Update, record);
        var collection = this.GetCollection();
        await collection.UpdateOneAsync(
                filter,
                update,
                null,
                cancellationToken);
        logger.UpdateCompleted();
    }

    protected abstract IMongoCollection<TDto> GetCollection();

    protected abstract FilterDefinition<TDto> ApplyFilterById(FilterDefinitionBuilder<TDto> filterBuilder, TId id);

    protected abstract TRecord MapFromDto(TDto dto);

    protected abstract TDto MapFromRecord(TRecord record);

    protected abstract UpdateDefinition<TDto> ApplyUpdateDefinition(UpdateDefinitionBuilder<TDto> builder, TRecord record);

    protected abstract TId GetIdFromRecord(TRecord record);
}

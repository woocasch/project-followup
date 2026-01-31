namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.EventSourcing;

public abstract class ProjectionWriterBase<TRecord, TDto, TId> : IProjectionWriter<TRecord, TId>
    where TRecord : struct
{
    public virtual async Task<TRecord?> Get(TId id, CancellationToken cancellationToken)
    {
        var collection = this.GetCollection();
        var filterBuilder = Builders<TDto>.Filter;
        var filter = this.ApplyFilterById(filterBuilder, id);
        var searchOptions = new FindOptions<TDto>()
        {
            Limit = 1,
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            return null;
        }

        var found = items[0];
        return this.MapFromDto(found);
    }

    public virtual async Task Insert(TRecord record, CancellationToken cancellationToken)
    {
        var dto = this.MapFromRecord(record);
        var collection = this.GetCollection();
        await collection.InsertOneAsync(
                dto,
                null,
                cancellationToken);
    }

    public virtual async Task Update(TRecord record, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<TDto>.Filter;
        var filter = this.ApplyFilterById(filterBuilder, this.GetIdFromRecord(record));
        var update = this.ApplyUpdateDefinition(Builders<TDto>.Update, record);
        var collection = this.GetCollection();
        await collection.UpdateOneAsync(
                filter,
                update,
                null,
                cancellationToken);
    }

    protected abstract IMongoCollection<TDto> GetCollection();

    protected abstract FilterDefinition<TDto> ApplyFilterById(FilterDefinitionBuilder<TDto> filterBuilder, TId id);

    protected abstract TRecord MapFromDto(TDto dto);

    protected abstract TDto MapFromRecord(TRecord record);

    protected abstract UpdateDefinition<TDto> ApplyUpdateDefinition(UpdateDefinitionBuilder<TDto> builder, TRecord record);

    protected abstract TId GetIdFromRecord(TRecord record);
}

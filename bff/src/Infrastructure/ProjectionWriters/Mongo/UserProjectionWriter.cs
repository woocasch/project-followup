namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo.UserProjection;

public sealed class UserProjectionWriter(
    ICollectionProvider collectionProvider) : IUserProjectionWriter
{
    public async Task<UserRecord?> Get(UserId id, CancellationToken cancellationToken)
    {
        var collection = this.GetCollection();
        var filterBuilder = Builders<UserDto>.Filter;
        var filter = filterBuilder.Eq(dto => dto.Id, id.Value);
        var searchOptions = new FindOptions<UserDto>()
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
        return new(
            UserId.FromGuid(found.Id),
            found.CredentialsId,
            EmailAddress.FromString(found.Email),
            found.DisplayName,
            found.CreatedAt);
    }

    public async Task Insert(UserRecord user, CancellationToken cancellationToken)
    {
        var dto = new UserDto
        {
            Id = user.Id.Value,
            CredentialsId = user.CredentialsId,
            Email = user.Email.Value,
            DisplayName = user.DisplayName,
            CreatedAt = user.CreatedAt,
        };
        var collection = this.GetCollection();
        await collection.InsertOneAsync(
                dto,
                null,
                cancellationToken);
    }

    public async Task Update(UserRecord user, CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<UserDto>.Filter;
        var filter = filterBuilder.Eq(dto => dto.Id, user.Id.Value);
        var update = Builders<UserDto>.Update
            .Set(u => u.CredentialsId, user.CredentialsId)
            .Set(u => u.Email, user.Email.ToString())
            .Set(u => u.DisplayName, user.DisplayName)
            .Set(u => u.CreatedAt, user.CreatedAt);
        var collection = this.GetCollection();
        await collection.UpdateOneAsync(
                filter,
                update,
                null,
                cancellationToken);
    }

    private IMongoCollection<UserDto> GetCollection()
    {
        return collectionProvider.Users;
    }
}

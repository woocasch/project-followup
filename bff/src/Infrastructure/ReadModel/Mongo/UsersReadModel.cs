namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.Application.Users.ReadModel;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

internal class UsersReadModel(
    ICollectionProvider collectionProvider,
    ILogger<UsersReadModel> logger) : IReadModel
{
    public async Task<UserRecord?> Get(UserId userId, CancellationToken cancellationToken)
    {
        logger.GetByIdStarted(userId.Value);
        var collection = collectionProvider.Users;
        var filter = Builders<UserDto>.Filter.Eq(p => p.Id, userId.ToGuid());
        var searchOptions = new FindOptions<UserDto>()
        {
            Limit = 1,
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        logger.GetByIdItemsRetrieved(userId.Value);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            logger.GetByIdUserNotFound(userId.Value);
            return null;
        }

        var found = items[0];
        logger.GetByIdCompleted(userId.Value);
        return new UserRecord
        {
            Id = UserId.FromGuid(found.Id),
            CredentialsId = found.CredentialsId,
            Email = EmailAddress.FromString(found.Email),
            DisplayName = found.DisplayName,
            CreatedAt = found.CreatedAt,
        };
    }

    public async Task<UserRecord?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        logger.GetByEmailStarted(email);
        var collection = collectionProvider.Users;
        var filter = Builders<UserDto>.Filter.Eq(p => p.Email, email);
        var searchOptions = new FindOptions<UserDto>()
        {
            Limit = 1,
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        logger.GetByEmailItemsRetrieved(email);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            logger.GetByEmailUserNotFound(email);
            return null;
        }

        var found = items[0];
        logger.GetByEmailCompleted(email);
        return new UserRecord
        {
            Id = UserId.FromGuid(found.Id),
            CredentialsId = found.CredentialsId,
            Email = EmailAddress.FromString(found.Email),
            DisplayName = found.DisplayName,
            CreatedAt = found.CreatedAt,
        };
    }
}

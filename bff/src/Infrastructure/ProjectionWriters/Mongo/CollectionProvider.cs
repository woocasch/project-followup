namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo.UserProjection;

public sealed class CollectionProvider(
    IDatabaseProvider databaseProvider) : ICollectionProvider
{
    private const string UsersCollectionName = "Users";

    public IMongoCollection<UserDto> Users => this.GetCollection<UserDto>(UsersCollectionName);
  
    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var database = databaseProvider.GetDatabase();
        return database.GetCollection<T>(collectionName);
    }
}

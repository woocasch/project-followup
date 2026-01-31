namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ActivationLinkProjection;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

public sealed class CollectionProvider(
    IDatabaseProvider databaseProvider) : ICollectionProvider
{
    private const string UsersCollectionName = "Users";

    private const string ActivationLinksCollectionName = "ActivationLinks";

    public IMongoCollection<UserDto> Users => this.GetCollection<UserDto>(UsersCollectionName);

    public IMongoCollection<ActivationLinkDto> ActivationLinks => this.GetCollection<ActivationLinkDto>(ActivationLinksCollectionName);

    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var database = databaseProvider.GetDatabase();
        return database.GetCollection<T>(collectionName);
    }
}

namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

public interface IDatabaseProvider
{
    IMongoDatabase GetDatabase();
}
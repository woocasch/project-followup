namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using MongoDB.Driver;

public interface IDatabaseProvider
{
    IMongoDatabase GetDatabase();
}
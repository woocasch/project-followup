namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using MongoDB.Driver;

public interface IClientProvider
{
    IMongoClient GetClient();
}
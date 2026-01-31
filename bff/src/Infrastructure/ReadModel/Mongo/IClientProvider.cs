namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

public interface IClientProvider
{
    IMongoClient GetClient();
}
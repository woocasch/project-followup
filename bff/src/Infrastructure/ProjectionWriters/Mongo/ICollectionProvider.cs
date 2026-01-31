namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo.UserProjection;

public interface ICollectionProvider
{
    IMongoCollection<UserDto> Users { get; }
}

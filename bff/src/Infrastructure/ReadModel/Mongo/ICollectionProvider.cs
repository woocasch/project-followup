namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ActivationLinkProjection;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ProjectProjection;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

public interface ICollectionProvider
{
    IMongoCollection<UserDto> Users { get; }

    IMongoCollection<ActivationLinkDto> ActivationLinks { get; }

    IMongoCollection<ProjectDto> Projects { get; }
}

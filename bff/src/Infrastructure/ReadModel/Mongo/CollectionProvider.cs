namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using MongoDB.Driver;

using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ActivationLinkProjection;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.ProjectProjection;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.TaskProjection;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo.UserProjection;

public sealed class CollectionProvider(
    IDatabaseProvider databaseProvider) : ICollectionProvider
{
    public const string UsersCollectionName = "Users";

    public const string ActivationLinksCollectionName = "ActivationLinks";

    public const string ProjectsCollectionName = "Projects";

    public const string TasksCollectionName = "Tasks";

    public IMongoCollection<UserDto> Users => this.GetCollection<UserDto>(UsersCollectionName);

    public IMongoCollection<ActivationLinkDto> ActivationLinks => this.GetCollection<ActivationLinkDto>(ActivationLinksCollectionName);

    public IMongoCollection<ProjectDto> Projects => this.GetCollection<ProjectDto>(ProjectsCollectionName);

    public IMongoCollection<TaskDto> Tasks => this.GetCollection<TaskDto>(TasksCollectionName);

    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var database = databaseProvider.GetDatabase();
        return database.GetCollection<T>(collectionName);
    }
}

namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

using ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoProjectionWriters(
        this IServiceCollection services)
    {
        services
            .AddProjectionWriters()
            .AddReadModels()
            .AddSingleton<IDatabaseProvider, DatabaseProvider>()
            .AddSingleton<ICollectionProvider, CollectionProvider>()
            .AddSingleton<IClientProvider, ClientProvider>()
            .AddSingleton<IMongoClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MongoSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));
        return services;
    }

    private static IServiceCollection AddProjectionWriters(
        this IServiceCollection services)
    {
        services
            .AddTransient<IUserProjectionWriter, UserProjectionWriter>()
            .AddTransient<IActivationLinkProjectionWriter, ActivationLinkProjectionWriter>()
            .AddTransient<IProjectProjectionWriter, ProjectProjectionWriter>()
            .AddTransient<ITaskProjectionWriter, TaskProjectionWriter>();
        return services;
    }

    private static IServiceCollection AddReadModels(
        this IServiceCollection services)
    {
        services
            .AddTransient<Application.ActivationLinks.IReadModel, ActivationLinksReadModel>()
            .AddTransient<Application.Projects.IReadModel, ProjectsReadModel>()
            .AddTransient<Application.Users.IReadModel, UsersReadModel>();
        return services;
    }
}

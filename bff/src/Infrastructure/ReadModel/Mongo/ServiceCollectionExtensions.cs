namespace ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoProjectionWriters(
        this IServiceCollection services)
    {
        services
            .AddTransient<IUserProjectionWriter, UserProjectionWriter>()
            .AddTransient<IActivationLinkProjectionWriter, ActivationLinkProjectionWriter>()
            .AddSingleton<IDatabaseProvider, DatabaseProvider>()
            .AddSingleton<ICollectionProvider, CollectionProvider>()
            .AddSingleton<IClientProvider, ClientProvider>()
            .AddTransient<Application.ActivationLinks.IReadModel, ActivationLinksReadModel>();
        return services;
    }
}

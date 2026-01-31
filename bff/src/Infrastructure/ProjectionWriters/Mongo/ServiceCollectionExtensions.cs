namespace ProjectFollowUp.BFF.Infrastructure.ProjectionWriters.Mongo;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoProjectionWriters(
        this IServiceCollection services)
    {
        services
            .AddTransient<IUserProjectionWriter, UserProjectionWriter>();
        return services;
    }
}

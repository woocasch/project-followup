namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel.MongoDb;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UserMongoReadModel(
        this IServiceCollection services)
    {
        services.AddSingleton<IUserManager, MongoUserManager>();
        return services;
    }
}

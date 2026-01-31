namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization.User;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Domain.User.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserMaterializers(
        this IServiceCollection services)
    {
        services.AddEventMaterializer<UserCreated, UserCreatedMaterializer>();
        return services;
    }
}

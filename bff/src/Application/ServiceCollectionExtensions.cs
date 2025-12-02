namespace ProjectFollowUp.BFF.Application;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Application.Users;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services
            .AddCqrs()
            .AddProjects()
            .AddUsers();
        return services;
    }
}

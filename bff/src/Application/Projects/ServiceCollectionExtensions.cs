namespace ProjectFollowUp.BFF.Application.Projects;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjects(
        this IServiceCollection services)
    {
        services.RegisterCommandHandler<CreateProjectCommand, CreateProjectCommandHandler>();
        return services;
    }
}

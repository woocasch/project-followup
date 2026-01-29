namespace ProjectFollowUp.BFF.Application;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
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
            .AddUsers()
            .AddActivationLinks()
            .AddSingleton<IAggregateFactory, AggregateFactory>();
        return services;
    }
}

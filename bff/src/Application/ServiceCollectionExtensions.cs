namespace ProjectFollowUp.BFF.Application;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Application.Tasks;
using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.Domain;

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
            .AddEventSourcing()
            .AddTasks()
            .AddSingleton<IAggregateFactory, AggregateFactory>();
        return services;
    }

    internal static IServiceCollection RegisterProjectionWorker<TAggregateEvent, TProjectionWorker>(
        this IServiceCollection services)
        where TAggregateEvent : IAggregateEvent
        where TProjectionWorker : class, IProjectionWorker
    {
        var key = typeof(TAggregateEvent).AssemblyQualifiedName!;
        services.AddKeyedTransient<IProjectionWorker, TProjectionWorker>(key);
        return services;
    }
}

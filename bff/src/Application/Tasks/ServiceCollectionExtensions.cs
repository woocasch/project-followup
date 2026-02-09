namespace ProjectFollowUp.BFF.Application.Tasks;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;
using ProjectFollowUp.BFF.Domain.Project.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTasks(
        this IServiceCollection services)
    {
        // CQRS handlers
        services
            .RegisterCommandHandler<CreateTaskCommand, CreateTaskCommandHandler>();

        // Projection workers
        services
            .RegisterProjectionWorker<TaskAdded, TaskAddedProjectionWorker>();
        return services;
    }
}

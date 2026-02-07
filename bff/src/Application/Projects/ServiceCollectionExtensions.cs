namespace ProjectFollowUp.BFF.Application.Projects;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;
using ProjectFollowUp.BFF.Domain.Project.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjects(
        this IServiceCollection services)
    {
        // CQRS handlers
        services
            .RegisterCommandHandler<CreateProjectCommand, CreateProjectCommandHandler>()
            .RegisterCommandHandler<UpdateProjectCommand, UpdateProjectCommandHandler>()
            .RegisterQueryHandler<FetchProjectsQuery, FetchProjectsResult, FetchProjectsQueryHandler>()
            .RegisterQueryHandler<GetProjectQuery, GetProjectResult, GetProjectQueryHandler>()
            .RegisterQueryHandler<FetchProjectUsersQuery, FetchProjectUsersResult, FetchProjectUsersQueryHandler>()
            .RegisterQueryHandler<FetchProjectTasksQuery, FetchProjectTasksResult, FetchProjectTasksQueryHandler>();

        // Projection workers
        services
            .RegisterProjectionWorker<ProjectCreated, ProjectCreatedProjectionWorker>()
            .RegisterProjectionWorker<ProjectDetailsChanged, ProjectDetailsChangedProjectionWorker>()
            .RegisterProjectionWorker<TaskAdded, TaskAddedProjectionWorker>()
            .RegisterProjectionWorker<TaskCompleted, TaskCompletedProjectionWorker>()
            .RegisterProjectionWorker<TaskRemoved, TaskRemovedProjectionWorker>()
            .RegisterProjectionWorker<TaskWorkStarted, TaskWorkStartedProjectionWorker>();
        return services;
    }
}

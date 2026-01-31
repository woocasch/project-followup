namespace ProjectFollowUp.BFF.Application.Users;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Users.ProjectionWorkers;
using ProjectFollowUp.BFF.Domain.User.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
        this IServiceCollection services)
    {
        // CQRS handlers
        services
            .RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();

        // Projection workers
        services
            .RegisterProjectionWorker<UserCreated, UserCreatedProjectionWorker>();
        return services;
    }
}

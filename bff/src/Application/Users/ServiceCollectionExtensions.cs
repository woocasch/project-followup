namespace ProjectFollowUp.BFF.Application.Users;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(
        this IServiceCollection services)
    {
        services
            .RegisterCommandHandler<CreateUserCommand, CreateUserCommandHandler>();
        return services;
    }
}

namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.Cqrs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddActivationLinks(
        this IServiceCollection services)
    {
        services
            .RegisterCommandHandler<CreateActivationLinkCommand, CreateActivationLinkCommandHandler>();
        return services;
    }
}

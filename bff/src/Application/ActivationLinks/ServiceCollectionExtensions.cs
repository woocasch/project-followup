namespace ProjectFollowUp.BFF.Application.ActivationLinks;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.ActivationLink.Events;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddActivationLinks(
        this IServiceCollection services)
    {
        // CQRS handlers
        services
            .RegisterCommandHandler<CreateActivationLinkCommand, CreateActivationLinkCommandHandler>()
            .RegisterQueryHandler<GetActivationLinkDataQuery, GetActivationLinkDataResult, GetActivationLinkDataQueryHandler>();

        // Projection workers
        services
            .RegisterProjectionWorker<LinkCreated, LinkCreatedProjectionWorker>();
        return services;
    }
}

namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider;

using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.IdentityProvider;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKeycloakIdentityProvider(
        this IServiceCollection services)
    {
        services
            .AddTransient<IIdentityProvider, KeycloakIdentityProvider>()
            .AddSingleton<IKeycloakClient, DefaultKeycloakClient>();
        return services;
    }
}

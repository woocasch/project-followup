namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider;

using global::Keycloak.Net;

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
            .AddScoped(sp =>
            {
                var settings = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<KeycloakSettings>>().Value;
                return new KeycloakClient(
                    settings.BaseAddress,
                    settings.ClientSecret,
                    new(authenticationRealm: settings.Realm, adminClientId: settings.ClientId));
            });
        return services;
    }
}

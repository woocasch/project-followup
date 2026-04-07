namespace ProjectFollowUp.BFF.KeycloakSetup;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.KeycloakSetup.Operations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOperationsImplementations(this IServiceCollection services)
    {
        services.AddTransient<IOperation, CreateRealm>();
        services.AddTransient<IOperation, CreateCustomRoles>();
        services.AddTransient<IOperation, CreateUserIdAttribute>();
        services.AddTransient<IOperation, CreateAdminUser>();
        services.AddTransient<IOperation, CreateUIClient>();
        services.AddTransient<IOperation, CreateBffClient>();
        return services;
    }

    public static IServiceCollection AddReporter<TReporter>(this IServiceCollection services)
        where TReporter : class, IReporter
    {
        services.AddTransient<IReporter, TReporter>();
        return services;
    }

    public static IServiceCollection AddKeycloakClient(this IServiceCollection services)
    {
        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<SetupSettings>>().Value;
            var keycloakServerUrl = settings.KeycloakBaseUrl;

            var keycloakClient = new Keycloak.Net.KeycloakClient(
                keycloakServerUrl,
                settings.MasterAdminUsername,
                settings.MasterAdminPassword,
                new(authenticationRealm: settings.MasterRealm));
            return keycloakClient;
        });
        return services;
    }
}

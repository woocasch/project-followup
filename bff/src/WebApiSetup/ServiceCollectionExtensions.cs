namespace ProjectFollowUp.BFF.WebApiSetup;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.WebApiSetup.Operations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOperationsImplementations(this IServiceCollection services)
    {
        services.AddTransient<IOperation, WaitForOtherSetups>();
        services.AddTransient<IOperation, SetupAdminUser>();
        return services;
    }

    public static IServiceCollection AddReporter<TReporter>(this IServiceCollection services)
        where TReporter : class, IReporter
    {
        services.AddTransient<IReporter, TReporter>();
        return services;
    }

    public static IServiceCollection AddKeycloakClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(sp =>
        {
            var keycloakBaseAddress = configuration.GetValue("Keycloak:BaseAddress", string.Empty);
            var settings = sp.GetRequiredService<IOptions<SetupSettings>>().Value;
            var keycloakClient = new Keycloak.Net.KeycloakClient(
                keycloakBaseAddress,
                settings.MasterAdminUsername,
                settings.MasterAdminPassword,
                new(authenticationRealm: settings.MasterRealm));
            return keycloakClient;
        });
        return services;
    }
}

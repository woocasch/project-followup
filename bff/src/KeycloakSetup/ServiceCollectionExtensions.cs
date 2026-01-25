namespace ProjectFollowUp.BFF.KeycloakSetup;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ProjectFollowUp.BFF.KeycloakSetup.Operations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOperationsImplementations(this IServiceCollection services)
    {
        services.AddTransient<IOperation, CreateRealm>();
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
        services.AddTransient(sp =>
        {
            var keycloakServerUrl = "http://localhost:4002";

            var keycloakClient = new Keycloak.Net.KeycloakClient(
                keycloakServerUrl,
                "admin",
                "admin",
                new(authenticationRealm: "master"));
            return keycloakClient;
        });
        return services;
    }

    public static IHostApplicationBuilder AddRealmSettings(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<RealmSettings>(builder.Configuration.GetSection("Keycloak"));
        return builder;
    }
}

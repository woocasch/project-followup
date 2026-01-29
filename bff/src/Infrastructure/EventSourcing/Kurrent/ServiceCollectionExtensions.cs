namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using KurrentDB.Client;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKurrentEventSourcing(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KurrentSettings>(configuration.GetSection("Kurrent"));
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<KurrentSettings>>();
            var clientSettings = KurrentDBClientSettings.Create(settings.Value.ConnectionString);
            return new KurrentDBClient(clientSettings);
        });
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<KurrentSettings>>();
            var clientSettings = KurrentDBClientSettings.Create(settings.Value.ConnectionString);
            return new KurrentDBProjectionManagementClient(clientSettings);
        });

        services.AddScoped<IEventStreamsRepository, KurrentEventStreamsRepository>();
        services.AddSingleton<INamingService, DefaultNamingService>();
        services.AddProjections();
        services.AddSingleton<IProjectionFactory, ProjectionFactory>();
        services.AddSingleton<IProjectionsInitializer, ProjectionsInitializer>();
        services.AddScoped<Application.Projects.IReadModel, ProjectsReadModel>();
        services.AddScoped<Application.ActivationLinks.IReadModel, ActivationLinksReadModel>();
        return services;
    }

    private static IServiceCollection AddProjections(
        this IServiceCollection services)
    {
        services
            .AddTransient<IProjection, UsersProjection>()
            .AddTransient<IProjection, ProjectsProjection>()
            .AddTransient<IProjection, ProjectsListProjection>()
            .AddTransient<IProjection, ActivationLinksProjection>();
        return services;
    }
}

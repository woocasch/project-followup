namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using KurrentDB.Client;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

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
            return new KurrentDBPersistentSubscriptionsClient(clientSettings);
        });
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<KurrentSettings>>();
            var clientSettings = KurrentDBClientSettings.Create(settings.Value.ConnectionString);
            return new KurrentDBProjectionManagementClient(clientSettings);
        });

        services.AddScoped<IEventStreamsRepository, KurrentEventStreamsRepository>();
        services.AddSingleton<INamingService, DefaultNamingService>();
        services.AddMaterializers();
        return services;
    }
}

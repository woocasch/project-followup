namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

using KurrentDB.Client;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ProjectFollowUp.BFF.Application.EventSourcing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKurrentEventSourcing(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("Kurrent:ConnectionString")!;

        var settings = KurrentDBClientSettings.Create(connectionString);
        var client = new KurrentDBClient(settings);

        services.AddSingleton(client);
        services.AddScoped<IEventStreamsRepository, KurrentEventStreamsRepository>();

        return services;
    }
}

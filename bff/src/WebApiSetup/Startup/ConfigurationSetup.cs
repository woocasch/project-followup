namespace ProjectFollowUp.BFF.WebApiSetup;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Application;
using ProjectFollowUp.BFF.Infrastructure.EventBus;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;
using ProjectFollowUp.BFF.Infrastructure.MailSender;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;

using RabbitMQ.Client;

public static class ConfigurationSetup
{
    public static IServiceCollection MapSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EventBusSettings>(configuration.GetSection("EventBus"));
        services.Configure<MongoSettings>(configuration.GetSection("Mongo"));
        return services;
    }

    public static IServiceCollection RegisterApplicationModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddKurrentEventSourcing(configuration)
            .AddMongoProjectionWriters();

        return services;
    }
}

namespace ProjectFollowUp.BFF.WebApi.MassTransit;

using global::MassTransit;

using Google.Api;

using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.Infrastructure.EventBus;
using ProjectFollowUp.BFF.Infrastructure.EventBus.Users;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMassTransit(
        this IServiceCollection services)
    {
        services.AddEventBus();
        services.AddMassTransit(
            configurator =>
            {
                configurator.AddUserConsumers();
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 5004, "projectfollowup-bff", c =>
                    {
                        c.Username("api-bff");
                        c.Password("api-bff");
                    });
                    context.AddUserConsumers(cfg);
                });
            });
        return services;
    }

    private static IBusRegistrationConfigurator AddUserConsumers(
        this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<SendActivationEmailConsumer>();
        return configurator;
    }

    private static IRabbitMqBusFactoryConfigurator AddUserConsumers(
        this IBusRegistrationContext context,
        IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<UserCreatedEvent>(x =>
        {
            x.SetEntityName("user.created");
        });
        configurator.ReceiveEndpoint("user.created.activation-email", e =>
        {
            e.ConfigureConsumer<SendActivationEmailConsumer>(context);
        });

        return configurator;
    }
}

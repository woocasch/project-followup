namespace ProjectFollowUp.BFF.WebApi.MassTransit;

using global::MassTransit;

using Google.Api;

using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Infrastructure.EventBus;
using ProjectFollowUp.BFF.Infrastructure.EventBus.Documents;
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
                configurator.AddActivationLinkConsumers();
                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", 5004, "projectfollowup-bff", c =>
                    {
                        c.Username("api-bff");
                        c.Password("api-bff");
                    });
                    context.AddUserConsumers(cfg);
                    context.AddActivationLinkConsumers(cfg);
                });
            });
        return services;
    }

    private static IBusRegistrationConfigurator AddUserConsumers(
        this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<CreateActivationLinkConsumer>();
        return configurator;
    }

    private static IBusRegistrationConfigurator AddActivationLinkConsumers(
        this IBusRegistrationConfigurator configurator)
    {
        configurator.AddConsumer<SendActivationMailConsumer>();
        return configurator;
    }

    private static IRabbitMqBusFactoryConfigurator AddUserConsumers(
        this IBusRegistrationContext context,
        IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<UserRegisteredEvent>(x =>
        {
            x.SetEntityName("user.created");
        });
        configurator.Message<ActivationLinkGeneratedEvent>(x =>
        {
            x.SetEntityName("activationlink.generated");
        });
        configurator.ReceiveEndpoint("user.created.activation-email", e =>
        {
            e.ConfigureConsumer<CreateActivationLinkConsumer>(context);
        });

        return configurator;
    }

    private static IRabbitMqBusFactoryConfigurator AddActivationLinkConsumers(
        this IBusRegistrationContext context,
        IRabbitMqBusFactoryConfigurator configurator)
    {
        configurator.Message<ActivationLinkGeneratedEvent>(x =>
        {
            x.SetEntityName("activationlink.generated");
        });
        configurator.ReceiveEndpoint("activationlink.generated.send-email", e =>
        {
            e.ConfigureConsumer<SendActivationMailConsumer>(context);
        });
        return configurator;
    }
}

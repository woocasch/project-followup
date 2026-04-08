namespace ProjectFollowUp.BFF.WebApi.Startup;

using System.Text.Json;
using System.Text.Json.Serialization;

using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

using ProjectFollowUp.BFF.Application;
using ProjectFollowUp.BFF.Infrastructure.EventBus;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;
using ProjectFollowUp.BFF.Infrastructure.MailSender;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;
using ProjectFollowUp.BFF.WebApi.EventsSubscriptions;
using ProjectFollowUp.BFF.WebApi.Middleware;
using ProjectFollowUp.BFF.WebApi.Validation;

public static class ConfigurationSetup
{
    public static IServiceCollection SetupControllers(
        this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        });

        return services;
    }

    public static IServiceCollection SetupOpenApi(
        this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static IServiceCollection SetupValidation(
        this IServiceCollection services)
    {
        services.AddValidation();
        return services;
    }

    public static IServiceCollection MapSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure Event Subscriptions
        services.Configure<EventBusSettings>(configuration.GetSection("EventBus"));
        services.Configure<QueueMappings>(configuration.GetSection("QueueMappings"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<MongoSettings>(configuration.GetSection("Mongo"));
        return services;
    }

    public static IServiceCollection SetupSerialization(
        this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.PropertyNameCaseInsensitive = true;
            options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        return services;
    }

    public static IServiceCollection SetupCache(
        this IServiceCollection services)
    {
        services
            .AddMemoryCache();
        return services;
    }

    public static IServiceCollection RegisterValidators(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateInput>, CreateInputValidator>();
        services.AddScoped<IValidator<UpdateInput>, UpdateInputValidator>();
        return services;
    }

    public static IServiceCollection RegisterApplicationModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddKurrentEventSourcing(configuration)
            .ConfigureEventsSubscriptions()
            .AddMailSender()
            .AddMongoProjectionWriters();

        return services;
    }

    public static WebApplication SetupErrorHandling(
        this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
        return app;
    }

    public static WebApplication SetupOpenApi(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        return app;
    }

    public static WebApplication SetupControllers(
        this WebApplication app)
    {
        app.MapControllers();
        return app;
    }
}

namespace ProjectFollowUp.BFF.WebApi.Startup;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using ProjectFollowUp.BFF.WebApi.HealthChecks;

public static class ObservabilitySetup
{
    public static IServiceCollection SetupOtel(
        this IServiceCollection services,
        ILoggingBuilder logging,
        IConfiguration configuration)
    {
        var serviceName = configuration.GetValue("OpenTelemetry:ServiceName", "UNKNOWN"); ;
        var serviceVersion = configuration.GetValue("OpenTelemetry:ServiceVersion", "X.X.X");
        var otlpEndpoint = configuration.GetValue("OpenTelemetry:OtlpEndpoint", string.Empty);
        var environment = configuration.GetValue("OpenTelemetry:Environment", "---");
        logging.ClearProviders();
        logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.ParseStateValues = true;
            options.IncludeScopes = true;
        });
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(serviceName: serviceName, serviceVersion: serviceVersion)
                .AddAttributes(new Dictionary<string, object>
                {
                    ["deployment.environment"] = environment,
                }))
            .WithLogging(logging => logging
                .AddOtlpExporter(options =>
                {
                    // OTLP gRPC endpoint
                    options.Endpoint = new Uri(otlpEndpoint);
                    options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                }))
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    // OTLP gRPC endpoint
                    options.Endpoint = new Uri(otlpEndpoint);
                    options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                }))
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddProcessInstrumentation()
                .AddRuntimeInstrumentation()
                .AddOtlpExporter(options =>
                {
                    // OTLP gRPC endpoint
                    options.Endpoint = new Uri(otlpEndpoint);
                    options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                }));

        return services;
    }

    public static IServiceCollection SetupHealthChecks(
        this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddCheck<KurrentHealthCheck>("kurrent")
            .AddCheck<MongoHealthCheck>("mongodb")
            .AddCheck<RabbitMqHealthCheck>("rabbitmq");
        return services;
    }

    public static WebApplication SetupHealthChecks(
        this WebApplication app)
    {
        app.MapHealthChecks("/health");
        return app;
    }
}

using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using FluentValidation;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using ProjectFollowUp.BFF.Application;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects;
using ProjectFollowUp.BFF.WebApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidation();

builder.Services.AddApplication();

builder.Services.AddKurrentEventSourcing(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<IValidator<CreateInput>, CreateInputValidator>();
builder.Services.AddScoped<IValidator<UpdateInput>, UpdateInputValidator>();
builder.Services.AddMemoryCache();

// Configure OpenTelemetry
var serviceName = builder.Configuration.GetValue("OpenTelemetry:ServiceName", "UNKNOWN"); ;
var serviceVersion = builder.Configuration.GetValue("OpenTelemetry:ServiceVersion", "X.X.X");
var otlpEndpoint = builder.Configuration.GetValue("OpenTelemetry:OtlpEndpoint", string.Empty);
var environment = builder.Configuration.GetValue("OpenTelemetry:Environment", "---");
builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeFormattedMessage = true;
    options.ParseStateValues = true;
    options.IncludeScopes = true;
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: serviceName, serviceVersion: serviceVersion)
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = environment,
        }))
    .WithLogging(logging => logging
        .AddConsoleExporter()
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
builder.Services.AddKeycloakIdentityProvider();
builder.Services.Configure<KeycloakSettings>(builder.Configuration.GetSection("Keycloak"));
var keycloakBaseAddress = builder.Configuration.GetValue("Keycloak:BaseAddress", string.Empty);
builder.Services.AddHttpClient("Keycloak", client =>
{
    client.BaseAddress = new Uri(keycloakBaseAddress);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials());

await app.StartAsync();

var usersProjection = app.Services.GetRequiredService<ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing.UsersProjection>();
await usersProjection.CreateProjection(CancellationToken.None);

await app.WaitForShutdownAsync();

namespace ProjectFollowUp.BFF.WebApi
{
    [CompilerGenerated]
    public sealed class WebApiProgram
    {
    }
}
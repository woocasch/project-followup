using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using ProjectFollowUp.BFF.Application;
using ProjectFollowUp.BFF.Infrastructure.EventBus;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;
using ProjectFollowUp.BFF.Infrastructure.MailSender;
using ProjectFollowUp.BFF.Infrastructure.ReadModel.Mongo;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;
using ProjectFollowUp.BFF.WebApi.EventsSubscriptions;
using ProjectFollowUp.BFF.WebApi.HealthChecks;
using ProjectFollowUp.BFF.WebApi.Middleware;
using ProjectFollowUp.BFF.WebApi.Security;
using ProjectFollowUp.BFF.WebApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Configure Security Settings
builder.Services.Configure<SecuritySettings>(builder.Configuration.GetSection("Security"));
var securitySettings = builder.Configuration.GetSection("Security").Get<SecuritySettings>() ?? new SecuritySettings();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddValidation();

builder.Services.AddApplication();

builder.Services.AddKurrentEventSourcing(builder.Configuration);

// Configure Event Subscriptions
builder.Services.Configure<EventBusSettings>(builder.Configuration.GetSection("EventBus"));
builder.Services.Configure<QueueMappings>(builder.Configuration.GetSection("QueueMappings"));
builder.Services.ConfigureEventsSubscriptions();

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

// Configure mail sender
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddMailSender();

// Configure Keycloak Settings
builder.Services.Configure<KeycloakSettings>(builder.Configuration.GetSection("Keycloak"));
var keycloakBaseAddress = builder.Configuration.GetValue("Keycloak:BaseAddress", string.Empty);
var keycloakRealm = builder.Configuration.GetValue("Keycloak:Realm", string.Empty);
var keycloakMetadataAddress = $"{keycloakBaseAddress.TrimEnd('/')}/realms/{keycloakRealm}/.well-known/openid-configuration";
var validIssuer = builder.Configuration.GetValue("Keycloak:ValidIssuer", string.Empty);


// Configure MongoDB
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
builder.Services.AddMongoProjectionWriters();
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

// Configure JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = validIssuer;
        options.RequireHttpsMetadata = securitySettings.RequireHttpsMetadata;
        options.MetadataAddress = keycloakMetadataAddress;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false, // Keycloak may not include audience in token
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ClockSkew = TimeSpan.FromMinutes(5)
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ProjectFollowUp.BFF.WebApi.WebApiProgram>>();
                logger.LogError(context.Exception, "Authentication failed");
                await Task.Yield();
            },
            OnTokenValidated = context =>
            {
                var identity = (ClaimsIdentity)context.Principal!.Identity!;

                var realmAccess = context.Principal.FindFirst("realm_access")?.Value;

                if (realmAccess != null)
                {
                    var json = JsonDocument.Parse(realmAccess);
                    if (json.RootElement.TryGetProperty("roles", out var roles))
                    {
                        foreach (var role in roles.EnumerateArray())
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()!));
                        }
                    }
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// Configure CORS policies
const string AllowAnyOriginPolicy = "AllowAnyOrigin";
const string AllowSpecificOriginsPolicy = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAnyOriginPolicy, policy =>
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());

    options.AddPolicy(AllowSpecificOriginsPolicy, policy =>
        policy
            .WithOrigins(securitySettings.Cors.AllowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

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
builder.Services.AddHttpClient("Keycloak", client =>
{
    client.BaseAddress = new Uri(keycloakBaseAddress);
});

// Configure Health Checks
builder.Services
    .AddHealthChecks()
    .AddCheck<KurrentHealthCheck>("kurrent")
    .AddCheck<MongoHealthCheck>("mongodb")
    .AddCheck<RabbitMqHealthCheck>("rabbitmq");

var app = builder.Build();

// Configure global exception handling
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Configure CORS based on settings
if (securitySettings.Cors.AllowAnyOrigin)
{
    app.UseCors(AllowAnyOriginPolicy);
}
else if (securitySettings.Cors.AllowedOrigins.Length > 0)
{
    app.UseCors(AllowSpecificOriginsPolicy);
}

app.UseAuthentication();
app.UseAuthorization();

// Map health check endpoints
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready");

app.MapControllers();

app.Run();

namespace ProjectFollowUp.BFF.WebApi
{
    [CompilerGenerated]
    public sealed class WebApiProgram
    {
    }
}
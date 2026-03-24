namespace ProjectFollowUp.BFF.WebApi.Startup;

using System.Security.Claims;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using ProjectFollowUp.BFF.Infrastructure.IdentityProvider;
using ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;
using ProjectFollowUp.BFF.WebApi.Security;

public static class SecuritySetup
{
    public const string AllowAnyOriginPolicy = "AllowAnyOrigin";
    
    public const string AllowSpecificOriginsPolicy = "AllowSpecificOrigins";

    public static IServiceCollection SetupAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure Keycloak Settings
        services.Configure<KeycloakSettings>(configuration.GetSection("Keycloak"));
        var keycloakBaseAddress = configuration.GetValue("Keycloak:BaseAddress", string.Empty);
        var keycloakRealm = configuration.GetValue("Keycloak:Realm", string.Empty);
        var keycloakMetadataAddress = $"{keycloakBaseAddress.TrimEnd('/')}/realms/{keycloakRealm}/.well-known/openid-configuration";
        var validIssuer = configuration.GetValue("Keycloak:ValidIssuer", string.Empty);
        var securitySettings = configuration.GetSecuritySettings();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                    },
                };
            });

        services.AddAuthorization();

        services.AddKeycloakIdentityProvider();
        services.AddHttpClient("Keycloak", client =>
        {
            client.BaseAddress = new Uri(keycloakBaseAddress);
        });
        
        return services;
    }

    public static IServiceCollection SetupCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure Security Settings
        services.Configure<SecuritySettings>(configuration.GetSection("Security"));
        var securitySettings = configuration.GetSecuritySettings();
        services.AddCors(options =>
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

        return services;
    }

    public static SecuritySettings GetSecuritySettings(this IConfiguration configuration)
    {
        return configuration.GetSection("Security").Get<SecuritySettings>() ?? new SecuritySettings();
    }

    public static WebApplication SetupCors(
        this WebApplication app)
    {
        var securitySettings = app.Configuration.GetSecuritySettings();
        if (securitySettings.Cors.AllowAnyOrigin)
        {
            app.UseCors(SecuritySetup.AllowAnyOriginPolicy);
        }
        else if (securitySettings.Cors.AllowedOrigins.Length > 0)
        {
            app.UseCors(SecuritySetup.AllowSpecificOriginsPolicy);
        }

        return app;
    }

    public static WebApplication SetupAuthentication(
        this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}

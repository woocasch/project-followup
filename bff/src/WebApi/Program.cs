using System.Runtime.CompilerServices;

using ProjectFollowUp.BFF.WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .SetupControllers()
    .SetupOpenApi()
    .SetupValidation()
    .SetupSerialization()
    .SetupCache()
    .SetupAuthentication(builder.Configuration)
    .SetupCors(builder.Configuration)
    .SetupOtel(builder.Logging, builder.Configuration)
    .SetupHealthChecks()
    .MapSettings(builder.Configuration)
    .RegisterValidators()
    .RegisterApplicationModules(builder.Configuration);

var app = builder.Build();

// Configure global exception handling
app
    .SetupErrorHandling()
    .SetupOpenApi()
    .SetupCors()
    .SetupAuthentication()
    .SetupHealthChecks()
    .SetupControllers();

app.Run();

namespace ProjectFollowUp.BFF.WebApi
{
    [CompilerGenerated]
    public sealed class WebApiProgram
    {
    }
}
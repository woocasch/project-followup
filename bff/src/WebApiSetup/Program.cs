using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ProjectFollowUp.BFF.WebApiSetup;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddUserSecrets(typeof(Program).Assembly, optional: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<SetupSettings>(builder.Configuration.GetSection("SetupSettings"));

builder.Services
    .MapSettings(builder.Configuration)
    .RegisterApplicationModules(builder.Configuration)
    .AddOperationsImplementations()
    .AddKeycloakClient(builder.Configuration)
    .AddReporter<ConsoleReporter>();

var app = builder.Build();

await RunOperations(app);

static async Task RunOperations(IHost app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var reporter = services.GetRequiredService<IReporter>();
    var operations = services.GetServices<IOperation>()
        .OrderBy(o => o.Order);

    reporter.Info("Starting configuration of Keycloak server...");

    foreach (var operation in operations)
    {
        reporter.Info($"Working on operation: '{operation.Description}'.");
        var isNeeded = await operation.IsNeeded(CancellationToken.None);
        if (isNeeded)
        {
            reporter.Info($"Executing operation: '{operation.Description}'");
            try
            {
                await operation.Execute(CancellationToken.None);
                reporter.Info($"Operation '{operation.Description}' completed.");
            }
            catch (Exception ex)
            {
                reporter.Error($"Operation '{operation.Description}' failed with exception: {ex.Message}");
                break;
            }
        }
        else
        {
            reporter.Warning($"Operation '{operation.Description}' is not needed. Skipping.");
        }
    }
}

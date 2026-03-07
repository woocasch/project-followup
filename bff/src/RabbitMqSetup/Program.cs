using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using ProjectFollowUp.BFF.RabbitMqSetup;

var builder = Host.CreateApplicationBuilder();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddUserSecrets(typeof(Program).Assembly, optional: true);
builder.Configuration.AddEnvironmentVariables();

var setupSettings = builder.Configuration.GetSection("SetupSettings").Get<SetupSettings>();

var reporter = new ConsoleReporter();

if (setupSettings is null)
{
    throw new InvalidOperationException("Failed to bind SetupSettings from configuration.");
}

var rabbitMqClient = SetupHelpers.CreateRabbitRestClient(setupSettings);

await OperationsRunner.RunOperations(
    rabbitMqClient,
    reporter,
    CancellationToken.None);
using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.KeycloakSetup;

var keycloakServerUrl = "http://localhost:4002";

var keycloakClient = new Keycloak.Net.KeycloakClient(
    keycloakServerUrl,
    "admin",
    "admin",
    new(authenticationRealm: "master"));

var realmSettings = Options.Create(new ProjectFollowUp.BFF.KeycloakSetup.RealmSettings
{
    RealmId = "project-follow-up",
    RealmName = "Project Follow Up",
});
var reporter = new ConsoleReporter();

var operations = new List<IOperation>
{
    new ProjectFollowUp.BFF.KeycloakSetup.Operations.CreateRealm(realmSettings, keycloakClient),
    new ProjectFollowUp.BFF.KeycloakSetup.Operations.CreateUIClient(realmSettings, keycloakClient, reporter),
    new ProjectFollowUp.BFF.KeycloakSetup.Operations.CreateBffClient(realmSettings, keycloakClient, reporter),
};

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
        }
    }
    else
    {
        reporter.Warning($"Operation '{operation.Description}' is not needed. Skipping.");
    }
}

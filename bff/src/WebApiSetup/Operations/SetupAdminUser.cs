namespace ProjectFollowUp.BFF.KeycloakSetup.Operations;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.WebApiSetup;

public sealed class SetupAdminUser(
    IReporter reporter,
    IReadModel readModel) : IOperation
{
    public int Order => 2;

    public string Description => "Setup of admin user";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating admin user information");
        await Task.Yield();
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        var user = await readModel.GetByEmail("projectfollowup", cancellationToken);
        return user is null;
    }
}

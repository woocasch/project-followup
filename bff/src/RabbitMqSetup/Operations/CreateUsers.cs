namespace ProjectFollowUp.BFF.RabbitMqSetup.Operations;

using System.Collections.ObjectModel;

internal sealed class CreateUsers(
    IRabbitMqClient rabbitMqClient,
    IReporter reporter)
    : IOperation
{
    private readonly ReadOnlyCollection<User> requiredUsers = new([
        new("api-bff", "api-bff"),
    ]);

    public int Order => 2;

    public string Description => "Creating users";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating users...");
        foreach (var user in requiredUsers)
        {
            if (await rabbitMqClient.UserExists(user.UserName, cancellationToken))
            {
                continue;
            }

            var created = await rabbitMqClient.CreateUser(user.UserName, user.Password, cancellationToken);
            if (created)
            {
                reporter.Info($"User '{user.UserName}' created.");
            }
            else
            {
                reporter.Error($"Failed to create user '{user.UserName}'.");
                throw new InvalidOperationException($"Failed to create user '{user.UserName}'.");
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        foreach (var user in requiredUsers)
        {
            if (!await rabbitMqClient.UserExists(user.UserName, cancellationToken))
            {
                return true;
            }
        }

        return false;
    }

    private record struct User(string UserName, string Password);
}

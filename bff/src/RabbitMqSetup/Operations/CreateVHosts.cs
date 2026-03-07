namespace ProjectFollowUp.BFF.RabbitMqSetup.Operations;

using System.Collections.ObjectModel;

public sealed class CreateVHosts(
    IRabbitMqClient rabbitMqClient,
    IReporter reporter)
    : IOperation
{
    private readonly ReadOnlyCollection<string> requiredVHosts = new([
        "projectfollowup",
    ]);

    public int Order => 1;

    public string Description => "Creating virtual hosts";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating virtual hosts...");
        foreach (var vhost in this.requiredVHosts)
        {
            if (await rabbitMqClient.VirtualHostExists(vhost, cancellationToken))
            {
                continue;
            }

            var created = await rabbitMqClient.CreateVirtualHost(vhost, cancellationToken);
            if (created)
            {
                reporter.Info($"Virtual host '{vhost}' created.");
            }
            else
            {
                reporter.Error($"Failed to create virtual host '{vhost}'.");
                throw new InvalidOperationException($"Failed to create virtual host '{vhost}'.");
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        foreach (var vhost in this.requiredVHosts)
        {
            if (!await rabbitMqClient.VirtualHostExists(vhost, cancellationToken))
            {
                return true;
            }
        }

        return false;
    }
}

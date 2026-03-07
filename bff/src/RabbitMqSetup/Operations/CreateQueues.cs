namespace ProjectFollowUp.BFF.RabbitMqSetup.Operations;

using System.Collections.ObjectModel;

public sealed class CreateQueues(
    IRabbitMqClient rabbitMqClient,
    IReporter reporter)
    : IOperation
{
    private readonly ReadOnlyCollection<Queue> queues = new([
        new("projectfollowup","QU.bff.activation-link.user.registered", "fanout" ,true ,false),
        new("projectfollowup" ,"QU.bff.documents.activation-link-generated", "fanout" ,true,false),
    ]);

    public int Order => 5;

    public string Description => "Creating queues";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating queues...");
        foreach (var queue in queues)
        {
            if (await rabbitMqClient.QueueExists(queue.VHost, queue.Name, cancellationToken))
            {
                continue;
            }

            var created = await rabbitMqClient.CreateQueue(
                queue.VHost,
                queue.Name,
                queue.Type,
                queue.Durable,
                queue.Autodelete,
                cancellationToken);
            if (created)
            {
                reporter.Info($"Queue '{queue.Name}' on vhost '{queue.VHost}' created.");
            }
            else
            {
                reporter.Error($"Failed to create queue '{queue.Name}' on vhost '{queue.VHost}'.");
                throw new InvalidOperationException($"Failed to create queue '{queue.Name}' on vhost '{queue.VHost}'.");
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        foreach (var queue in queues)
        {
            if (!await rabbitMqClient.QueueExists(queue.VHost, queue.Name, cancellationToken))
            {
                return true;
            }
        }

        return false;
    }

    private record struct Queue(
        string VHost,
        string Name,
        string Type,
        bool Durable,
        bool Autodelete);
}

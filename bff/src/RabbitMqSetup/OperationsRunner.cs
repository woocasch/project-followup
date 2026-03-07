namespace ProjectFollowUp.BFF.RabbitMqSetup;

using System.Collections.ObjectModel;

public static class OperationsRunner
{
    private delegate IOperation OperationFactory(IRabbitMqClient rabbitMqClient, IReporter reporter);

    private static readonly ReadOnlyCollection<OperationFactory> operationFactories = new([
        (rabbitMqClient, reporter) => new Operations.CreateVHosts(rabbitMqClient, reporter),
        (rabbitMqClient, reporter) => new Operations.CreateUsers(rabbitMqClient, reporter),
        (rabbitMqClient, reporter) => new Operations.SetPermissions(rabbitMqClient, reporter),
        (rabbitMqClient, reporter) => new Operations.CreateExchanges(rabbitMqClient, reporter),
        (rabbitMqClient, reporter) => new Operations.CreateQueues(rabbitMqClient, reporter),
        (rabbitMqClient, reporter) => new Operations.CreateBindings(rabbitMqClient, reporter),
    ]);

    public static async Task RunOperations(IRabbitMqClient rabbitMqClient, CancellationToken cancellationToken)
    {
        await RunOperations(rabbitMqClient, new ConsoleReporter(), cancellationToken);
    }

    public static async Task RunOperations(IRabbitMqClient rabbitMqClient, IReporter reporter, CancellationToken cancellationToken)
    {
        reporter.Info("Starting configuration of RabbitMQ server...");
        foreach (var operation in operationFactories.Select(f => f(rabbitMqClient, reporter)).OrderBy(o => o.Order))
        {
            reporter.Info($"Working on operation: '{operation.Description}'.");
            try
            {
                var isNeeded = await operation.IsNeeded(cancellationToken);
                if (!isNeeded)
                {
                    reporter.Warning($"Operation '{operation.Description}' is not needed. Skipping.");
                    continue;
                }

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
            catch (Exception ex)
            {
                reporter.Error($"Failed to check if operation '{operation.Description}' is needed with exception: {ex.Message}");
                break;
            }
        }
    }
}

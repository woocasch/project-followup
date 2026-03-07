namespace ProjectFollowUp.BFF.RabbitMqSetup.Operations;

using System.Collections.ObjectModel;

public sealed class CreateExchanges(
    IRabbitMqClient rabbitMqClient,
    IReporter reporter)
    : IOperation
{
    private readonly ReadOnlyCollection<Exchange> exchanges = new([
        new("projectfollowup", "EX.bff.user.registered", "fanout", true ,false),
        new("projectfollowup","EX.bff.activation-link.generated" ,"fanout",true ,false),
    ]);

    public int Order => 4;

    public string Description => "Creating exchanges";

    public async Task Execute(CancellationToken cancellationToken)
    {
        reporter.Info("Creating exchanges...");
        foreach (var exchange in exchanges)
        {
            if (await rabbitMqClient.ExchangeExists(exchange.VHost, exchange.Name, cancellationToken))
            {
                continue;
            }

            var created = await rabbitMqClient.CreateExchange(
                exchange.VHost,
                exchange.Name,
                exchange.Type,
                exchange.Durable,
                exchange.Autodelete,
                cancellationToken);
            if (created)
            {
                reporter.Info($"Exchange '{exchange.Name}' on vhost '{exchange.VHost}' created.");
            }
            else
            {
                reporter.Error($"Failed to create exchange '{exchange.Name}' on vhost '{exchange.VHost}'.");
                throw new InvalidOperationException($"Failed to create exchange '{exchange.Name}' on vhost '{exchange.VHost}'.");
            }
        }
    }

    public async Task<bool> IsNeeded(CancellationToken cancellationToken)
    {
        foreach (var exchange in exchanges)
        {
            if (!await rabbitMqClient.ExchangeExists(exchange.VHost, exchange.Name, cancellationToken))
            {
                return true;
            }
        }

        return false;
    }

    private record struct Exchange(
        string VHost,
        string Name,
        string Type,
        bool Durable,
        bool Autodelete);
}

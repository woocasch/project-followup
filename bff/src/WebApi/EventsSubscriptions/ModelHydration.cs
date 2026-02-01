namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using MimeKit.Cryptography;

using ProjectFollowUp.BFF.Infrastructure.EventBus;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

public sealed class ModelHydration(
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await this.Configure(serviceProvider, stoppingToken);
        await this.StartHydration(serviceProvider, stoppingToken);
    }

    private async Task Configure(
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var client = serviceProvider.GetRequiredService<KurrentDBPersistentSubscriptionsClient>();
        var subscriptionExists = true;
        try
        {
            await client.GetInfoToAllAsync("read-model-hydrator-subscription", cancellationToken: cancellationToken);
        }
        catch(PersistentSubscriptionNotFoundException)
        {
            subscriptionExists = false;
        }

        if (!subscriptionExists)
        {
            await client.CreateToAllAsync(
                "read-model-hydrator-subscription",
                new PersistentSubscriptionSettings(
                    resolveLinkTos: true,
                    startFrom: Position.Start,
                    extraStatistics: false,
                    messageTimeout: TimeSpan.FromSeconds(30),
                    maxRetryCount: 10,
                    liveBufferSize: 500,
                    readBatchSize: 20,
                    historyBufferSize: 500,
                    checkPointAfter: TimeSpan.FromSeconds(2),
                    checkPointLowerBound: 10,
                    checkPointUpperBound: 1000),
                cancellationToken: cancellationToken);
        }
    }

    private async Task<IReadModelHydrator> StartHydration(IServiceProvider serviceProvider, CancellationToken stoppingToken)
    {
        var hydrator = serviceProvider.GetRequiredService<IReadModelHydrator>();
        await hydrator.Subscribe(stoppingToken);
        return hydrator;
    }
}

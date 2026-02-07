namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

public sealed class ReadModelHydration(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<KurrentSettings> kurrentSettingsOptions) : BackgroundService
{
    private KurrentSettings Settings => kurrentSettingsOptions.Value;

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
        PersistentSubscriptionInfo? info = null;
        try
        {
            info = await client.GetInfoToAllAsync(
                this.Settings.ReadModelHydration.SubscriptionName,
                cancellationToken: cancellationToken);
        }
        catch(PersistentSubscriptionNotFoundException ex)
        {
            subscriptionExists = false;
            Console.WriteLine(ex);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }

        if (!subscriptionExists)
        {
            await client.CreateToAllAsync(
                this.Settings.ReadModelHydration.SubscriptionName,
                this.CreateSubscriptionSettings(),
                cancellationToken: cancellationToken);
        }
        else if (info?.Settings is not null)
        {
            if (info.Settings.CheckPointAfter != TimeSpan.FromMilliseconds(this.Settings.ReadModelHydration.CheckpointAfterMs)
                || info.Settings.CheckPointLowerBound != this.Settings.ReadModelHydration.CheckpointLowerBound
                || info.Settings.CheckPointUpperBound != this.Settings.ReadModelHydration.CheckpointUpperBound)
            {
                await client.UpdateToAllAsync(
                    this.Settings.ReadModelHydration.SubscriptionName,
                    this.CreateSubscriptionSettings(),
                    cancellationToken: cancellationToken);
            }
        }
    }

    private PersistentSubscriptionSettings CreateSubscriptionSettings()
    {
        var subscriptionSettings = this.Settings.ReadModelHydration;
        return new PersistentSubscriptionSettings(
                            resolveLinkTos: true,
                            startFrom: Position.Start,
                            extraStatistics: false,
                            messageTimeout: TimeSpan.FromSeconds(30),
                            maxRetryCount: 10,
                            liveBufferSize: 500,
                            readBatchSize: 20,
                            historyBufferSize: 500,
                            checkPointAfter: TimeSpan.FromMilliseconds(subscriptionSettings.CheckpointAfterMs),
                            checkPointLowerBound: subscriptionSettings.CheckpointLowerBound,
                            checkPointUpperBound: subscriptionSettings.CheckpointUpperBound);
    }

    private async Task<IReadModelHydrator> StartHydration(IServiceProvider serviceProvider, CancellationToken stoppingToken)
    {
        var hydrator = serviceProvider.GetRequiredService<IReadModelHydrator>();
        await hydrator.Subscribe(stoppingToken);
        return hydrator;
    }
}

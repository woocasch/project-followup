namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

using System.Threading;
using System.Threading.Tasks;

using KurrentDB.Client;

using Microsoft.Extensions.Options;

using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;
using ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

public sealed class ReadModelHydration(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<KurrentSettings> kurrentSettingsOptions,
    ILogger<ReadModelHydration> logger)
    : BackgroundService
{
    private KurrentSettings Settings => kurrentSettingsOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Starting();
        using var scope = serviceScopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await this.Configure(serviceProvider, stoppingToken);
        logger.ConfigurationCompleted();
        await this.StartHydration(serviceProvider, stoppingToken);
        logger.ExitingSubscription();
    }

    private async Task Configure(
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var client = serviceProvider.GetRequiredService<KurrentDBPersistentSubscriptionsClient>();
        logger.ClientAquired();
        var subscriptionExists = true;
        PersistentSubscriptionInfo? info = null;
        try
        {
            logger.CheckingSubscriptionExists();
            info = await client.GetInfoToAllAsync(
                this.Settings.ReadModelHydration.SubscriptionName,
                cancellationToken: cancellationToken);
        }
        catch(PersistentSubscriptionNotFoundException)
        {
            subscriptionExists = false;
        }
        catch (Exception ex)
        {
            // No log needed. This is required, as there's no clean way of checking if the subscription exists,
            // other than trying to get it and catching the exception if it doesn't.
        }

        if (!subscriptionExists)
        {
            logger.CreatingSubscription();
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
                logger.UpdatingSubscription();
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
        logger.StartingHydrator();
        await hydrator.Subscribe(stoppingToken);
        return hydrator;
    }
}

namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Infrastructure.EventBus;

public sealed class SubscriptionsManager(
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var consumers = (await scope.ServiceProvider.BindAllConsumers(stoppingToken))
            .ToList();
        while(!stoppingToken.IsCancellationRequested)
        {
            try
            {
                consumers.ForEach(Configure);
            }
            catch
            {
                // Log exception.
            }
            finally
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }

    private void Configure(IConsumer consumer)
    {
        // Handle errors and unbound consumer.
    }
}

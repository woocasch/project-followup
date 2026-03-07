namespace ProjectFollowUp.BFF.WebApi.EventsSubscriptions;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Infrastructure.EventBus;

public sealed class SubscriptionsManager(
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var consumers = (await scope.ServiceProvider.BindAllConsumers(stoppingToken))
            .ToList();
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), CancellationToken.None);
        }

        foreach(var consumer in consumers)
        {
            consumer.UnbindFromQueue();
        }
    }
}

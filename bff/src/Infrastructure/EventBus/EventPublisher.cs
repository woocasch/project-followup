namespace ProjectFollowUp.BFF.Infrastructure.EventBus;

using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using ProjectFollowUp.BFF.Application.EventsBus;

public sealed class EventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint publishEndpoint;

    public EventPublisher(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public async Task Publish(object @event, CancellationToken cancellationToken)
    {
        await this.publishEndpoint.Publish(@event, cancellationToken);
    }
}

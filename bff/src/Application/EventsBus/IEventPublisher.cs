namespace ProjectFollowUp.BFF.Application.EventsBus;

public interface IEventPublisher
{
    Task Publish(object @event, CancellationToken cancellationToken);
}

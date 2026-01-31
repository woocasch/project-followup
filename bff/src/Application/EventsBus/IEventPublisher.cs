namespace ProjectFollowUp.BFF.Application.EventsBus;

using ProjectFollowUp.BFF.Domain;

public interface IEventPublisher
{
    Task Publish(IDomainEvent @event, CancellationToken cancellationToken);
}

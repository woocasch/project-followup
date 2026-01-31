namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

public interface IEventMaterializer
{
    Task Materialize(
        object @event,
        CancellationToken cancellationToken);
}

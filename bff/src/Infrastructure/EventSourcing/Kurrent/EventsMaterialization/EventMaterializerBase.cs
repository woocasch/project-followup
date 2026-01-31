namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

using System.Threading;
using System.Threading.Tasks;

public abstract class EventMaterializerBase<T> : IEventMaterializer
{
    public async Task Materialize(object @event, CancellationToken cancellationToken)
    {
        if (@event is not T typedEvent)
        {
            throw new InvalidCastException($"Invalid event type. Expected {typeof(T).FullName}, but received {@event.GetType().FullName}.");
        }

        await this.MaterializeEvent(typedEvent, cancellationToken);
    }

    protected abstract Task MaterializeEvent(
            T @event,
            CancellationToken cancellationToken);
}

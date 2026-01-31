namespace ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink.Events;

public sealed class LinkCreatedProjectionWorker(
    IActivationLinkProjectionWriter projectionWriter) : ProjectionWorkerBase<LinkCreated>
{
    protected override async Task Materialize(LinkCreated domainEvent, CancellationToken cancellationToken)
    {
        var activationLink = await projectionWriter.Get(domainEvent.LinkId, cancellationToken);
        if (activationLink is not null)
        {
            activationLink = activationLink.Value with
            {
                UserId = domainEvent.UserId,
                LinkCode = domainEvent.LinkCode
            };
            await projectionWriter.Update(activationLink.Value, cancellationToken);
        }
        else
        {
            activationLink = new(
                domainEvent.LinkId,
                domainEvent.UserId,
                domainEvent.LinkCode);
            await projectionWriter.Insert(activationLink.Value, cancellationToken);
        }
    }
}

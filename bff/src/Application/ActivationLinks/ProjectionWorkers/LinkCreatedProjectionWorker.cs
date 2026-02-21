namespace ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink.Events;

public sealed class LinkCreatedProjectionWorker(
    IActivationLinkProjectionWriter projectionWriter,
    ILogger<LinkCreatedProjectionWorker> logger) : ProjectionWorkerBase<LinkCreated>
{
    protected override async Task Materialize(LinkCreated domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.LinkId.Value);
        var activationLink = await projectionWriter.Get(domainEvent.LinkId, cancellationToken);
        if (activationLink is not null)
        {
            logger.UpdatingExistingLink(domainEvent.LinkId.Value);
            activationLink = activationLink.Value with
            {
                UserId = domainEvent.UserId,
                LinkCode = domainEvent.LinkCode
            };
            await projectionWriter.Update(activationLink.Value, cancellationToken);
        }
        else
        {
            logger.CreatingNewLink(domainEvent.LinkId.Value);
            activationLink = new(
                domainEvent.LinkId,
                domainEvent.UserId,
                domainEvent.LinkCode);
            await projectionWriter.Insert(activationLink.Value, cancellationToken);
        }

        logger.Completed(domainEvent.LinkId.Value);
    }
}

namespace ProjectFollowUp.BFF.Application.ActivationLinks.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.ActivationLink.Events;

public sealed class LinkCreatedProjectionWorker : ProjectionWorkerBase<LinkCreated>
{
    protected override async Task Materialize(LinkCreated domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Activation link '{domainEvent.LinkCode}' created for user '{domainEvent.UserId}'.");
        await Task.Yield();
    }
}

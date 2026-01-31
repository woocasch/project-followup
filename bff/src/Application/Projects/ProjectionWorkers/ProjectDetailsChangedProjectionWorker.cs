namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectDetailsChangedProjectionWorker : ProjectionWorkerBase<ProjectDetailsChanged>
{
    protected override async Task Materialize(ProjectDetailsChanged domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Project details changed for ProjectId: {domainEvent.ProjectId}, Title: {domainEvent.Title}, Description: {domainEvent.Description}, ChangedAt: {domainEvent.ChangedAt}");
        await Task.Yield();
    }
}

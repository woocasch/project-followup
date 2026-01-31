namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectCreatedProjectionWorker : ProjectionWorkerBase<ProjectCreated>
{
    protected override async Task Materialize(ProjectCreated domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Project created: {0} - {1}", domainEvent.ProjectId, domainEvent.Title);
        await Task.Yield();
    }
}

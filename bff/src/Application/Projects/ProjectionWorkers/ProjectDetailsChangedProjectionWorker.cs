namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectDetailsChangedProjectionWorker(
    IProjectProjectionWriter projectionWriter) : ProjectionWorkerBase<ProjectDetailsChanged>
{
    protected override async Task Materialize(ProjectDetailsChanged domainEvent, CancellationToken cancellationToken)
    {
        var project = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (project is null)
        {
            throw new InvalidOperationException(
                $"Project with ID '{domainEvent.ProjectId}' not found for updating details.");
        }

        project = project.Value with
        {
            Title = domainEvent.Title,
            Description = domainEvent.Description,
        };
        await projectionWriter.Update(project.Value, cancellationToken);
    }
}

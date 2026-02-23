namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectDetailsChangedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    ILogger<ProjectDetailsChangedProjectionWorker> logger) : ProjectionWorkerBase<ProjectDetailsChanged>
{
    protected override async Task Materialize(ProjectDetailsChanged domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.ProjectId.Value);
        var project = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (project is null)
        {
            logger.ProjectNotFound(domainEvent.ProjectId.Value);
            throw new InvalidOperationException(
                $"Project with ID '{domainEvent.ProjectId}' not found for updating details.");
        }

        project = project.Value with
        {
            Title = domainEvent.Title,
            Description = domainEvent.Description,
        };
        logger.UpdatingProject(domainEvent.ProjectId.Value);
        await projectionWriter.Update(project.Value, cancellationToken);
        logger.Completed(domainEvent.ProjectId.Value);
    }
}

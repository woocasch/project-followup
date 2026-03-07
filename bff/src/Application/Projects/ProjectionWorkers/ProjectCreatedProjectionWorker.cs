namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectCreatedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    ILogger<ProjectCreatedProjectionWorker> logger) : ProjectionWorkerBase<ProjectCreated>
{
    protected override async Task Materialize(ProjectCreated domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(typeof(ProjectCreated), domainEvent.ProjectId.Value);
        var project = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (project is not null)
        {
            logger.ProjectAlreadyExists(domainEvent.ProjectId.Value);
            project = project.Value with
            {
                Title = domainEvent.Title,
                Description = domainEvent.Description,
                CreatedAt = domainEvent.CreatedAt,
            };
            await projectionWriter.Update(project.Value, cancellationToken);
            logger.ProjectUpdated(domainEvent.ProjectId.Value);
        }
        else
        {
            logger.ProjectNotExists(domainEvent.ProjectId.Value);
            project = new ProjectRecord(
                domainEvent.ProjectId,
                domainEvent.Title,
                domainEvent.Description,
                domainEvent.CreatedAt,
                [],
                []);
            await projectionWriter.Insert(project.Value, cancellationToken);
            logger.ProjectInserted(domainEvent.ProjectId.Value);
        }
    }
}

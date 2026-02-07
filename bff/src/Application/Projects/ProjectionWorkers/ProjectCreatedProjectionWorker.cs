namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectCreatedProjectionWorker(
    IProjectProjectionWriter projectionWriter) : ProjectionWorkerBase<ProjectCreated>
{
    protected override async Task Materialize(ProjectCreated domainEvent, CancellationToken cancellationToken)
    {
        var project = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (project is not null)
        {
            project = project.Value with
            {
                Title = domainEvent.Title,
                Description = domainEvent.Description,
                CreatedAt = domainEvent.CreatedAt,
            };
            await projectionWriter.Update(project.Value, cancellationToken);
        }
        else
        {
            project = new ProjectRecord(
                domainEvent.ProjectId,
                domainEvent.Title,
                domainEvent.Description,
                domainEvent.CreatedAt,
                []);
            await projectionWriter.Insert(project.Value, cancellationToken);
        }
    }
}

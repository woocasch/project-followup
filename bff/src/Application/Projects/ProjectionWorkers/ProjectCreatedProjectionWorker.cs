namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

using IUserReadModel = ProjectFollowUp.BFF.Application.Users.IReadModel;

public sealed class ProjectCreatedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    IUserReadModel userReadModel,
    ILogger<ProjectCreatedProjectionWorker> logger) : ProjectionWorkerBase<ProjectCreated>
{
    protected override async Task Materialize(ProjectCreated domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(typeof(ProjectCreated), domainEvent.ProjectId.Value);
        var project = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        var user = await userReadModel.Get(domainEvent.CreatedBy, cancellationToken);
        if (user is null)
        {
            logger.CreatorNotFoundInReadModel(domainEvent.CreatedBy.Value, domainEvent.ProjectId.Value);
            throw new InvalidOperationException($"Creator with id {domainEvent.CreatedBy.Value} not found in read model for project {domainEvent.ProjectId.Value}");
        }

        if (project is not null)
        {
            logger.ProjectAlreadyExists(domainEvent.ProjectId.Value);
            project = project.Value with
            {
                Title = domainEvent.Title,
                Description = domainEvent.Description,
                CreatedBy = domainEvent.CreatedBy,
                CreatedAt = domainEvent.CreatedAt,
                AssignedUsers = [new(user.Value.Id, user.Value.DisplayName, ProjectUser.RoleInProject.Owner.ToString())]
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
                domainEvent.CreatedBy,
                domainEvent.CreatedAt,
                [new(user.Value.Id, user.Value.DisplayName, ProjectUser.RoleInProject.Owner.ToString())],
                []);
            await projectionWriter.Insert(project.Value, cancellationToken);
            logger.ProjectInserted(domainEvent.ProjectId.Value);
        }
    }
}

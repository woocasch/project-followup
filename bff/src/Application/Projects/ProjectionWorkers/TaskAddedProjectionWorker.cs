namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskAddedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    ILogger<TaskAddedProjectionWorker> logger)
    : ProjectionWorkerBase<TaskAdded>
{
    protected override async Task Materialize(TaskAdded domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.ProjectId.Value, domainEvent.TaskId);
        var rawProject = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (rawProject is null)
        {
            logger.ProjectNotFound(domainEvent.ProjectId.Value);
            var message = $"Project with id {domainEvent.ProjectId.Value} not found.";
            throw new InvalidOperationException(message);
        }

        var project = rawProject.Value;
        var currentTasks = project.Tasks;
        var existing = currentTasks.SingleOrDefault(t => t.Id == domainEvent.TaskId);
        var existingIndex = currentTasks.IndexOf(existing);
        if (existing.Id == Guid.Empty)
        {
            existing = new ProjectRecord.Task(
                domainEvent.TaskId,
                domainEvent.Title,
                domainEvent.DueDate,
                ProjectTaskStatus.Created);
            currentTasks.Add(existing);
            logger.TaskAdded(domainEvent.ProjectId.Value, domainEvent.TaskId);
        }
        else
        {
            existing = existing with
            {
                Title = domainEvent.Title,
                DueDate = domainEvent.DueDate,
                Status = ProjectTaskStatus.Created,
            };
            currentTasks[existingIndex] = existing;
            logger.TaskUpdated(domainEvent.ProjectId.Value, domainEvent.TaskId);
        }

        project = project with
        {
            Tasks = currentTasks,
        };
        logger.SavingUpdatedProject(domainEvent.ProjectId.Value);
        await projectionWriter.Update(project, cancellationToken);
        logger.Completed(domainEvent.ProjectId.Value, domainEvent.TaskId);
    }
}

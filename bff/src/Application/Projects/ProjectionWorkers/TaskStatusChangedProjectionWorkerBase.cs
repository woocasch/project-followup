namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public abstract class TaskStatusChangedProjectionWorkerBase<TAggregateEvent>(
    IProjectProjectionWriter projectionWriter,
    ILogger<ITaskStatusChangedProjectionWorker> logger)
    : ProjectionWorkerBase<TAggregateEvent>, ITaskStatusChangedProjectionWorker
    where TAggregateEvent : IAggregateEvent, IProjectTaskEvent
{
    protected abstract ProjectTaskStatus TargetStatus { get; }

    protected override async Task Materialize(TAggregateEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.ProjectId.Value, domainEvent.TaskId, this.TargetStatus);
        var rawProject = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (rawProject is null)
        {
            logger.ProjectNotFound(domainEvent.ProjectId.Value);
            return;
        }

        var project = rawProject.Value;
        var currentTasks = project.Tasks;
        var existing = currentTasks.SingleOrDefault(t => t.Id == domainEvent.TaskId);
        if (existing.Id == default)
        {
            logger.TaskNotFound(domainEvent.ProjectId.Value, domainEvent.TaskId);
            return;
        }

        var existingIndex = currentTasks.IndexOf(existing);
        existing = existing with
        {
            Status = this.TargetStatus
        };
        currentTasks[existingIndex] = existing;
        project = project with { Tasks = currentTasks };
        logger.SendingUpdate(domainEvent.ProjectId.Value);
        await projectionWriter.Update(project, cancellationToken);
        logger.Completed(domainEvent.ProjectId.Value, domainEvent.TaskId);
    }
}

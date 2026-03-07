namespace ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Tasks.ReadModel;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskAddedProjectionWorker(
    ITaskProjectionWriter projectionWriter,
    ILogger<TaskAddedProjectionWorker> logger) : ProjectionWorkerBase<TaskAdded>
{
    protected override async Task Materialize(TaskAdded domainEvent, CancellationToken cancellationToken)
    {
        logger.Started(domainEvent.ProjectId.Value, domainEvent.TaskId);
        var existing = await projectionWriter.Get(domainEvent.TaskId, cancellationToken);
        if (existing is not null)
        {
            logger.UpdatingExistingProjection(domainEvent.ProjectId.Value, domainEvent.TaskId);
            var updated = existing.Value with
            {
                ProjectId = domainEvent.ProjectId,
                Title = domainEvent.Title,
                Description = domainEvent.Description,
                DueDate = domainEvent.DueDate,
                Status = domainEvent.Status,
                CreatedAt = domainEvent.CreatedAt
            };
            await projectionWriter.Update(updated, cancellationToken);
            logger.ProjectionUpdated(domainEvent.ProjectId.Value, domainEvent.TaskId);
            return;
        }

        logger.CreatingNewProjection(domainEvent.ProjectId.Value, domainEvent.TaskId);
        var projection = new TaskRecord(
            domainEvent.TaskId,
            domainEvent.ProjectId,
            domainEvent.Title,
            domainEvent.Description,
            domainEvent.DueDate,
            domainEvent.Status,
            domainEvent.CreatedAt);
        await projectionWriter.Insert(projection, cancellationToken);
        logger.ProjectionCreated(domainEvent.ProjectId.Value, domainEvent.TaskId);
    }
}

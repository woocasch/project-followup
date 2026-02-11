namespace ProjectFollowUp.BFF.Application.Tasks.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Tasks.ReadModel;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskAddedProjectionWorker(
    ITaskProjectionWriter projectionWriter) : ProjectionWorkerBase<TaskAdded>
{
    protected override async Task Materialize(TaskAdded domainEvent, CancellationToken cancellationToken)
    {
        var existing = await projectionWriter.Get(domainEvent.TaskId, cancellationToken);
        if (existing is not null)
        {
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
            return;
        }

        var projection = new TaskRecord(
            domainEvent.TaskId,
            domainEvent.ProjectId,
            domainEvent.Title,
            domainEvent.Description,
            domainEvent.DueDate,
            domainEvent.Status,
            domainEvent.CreatedAt);
        await projectionWriter.Insert(projection, cancellationToken);
    }
}

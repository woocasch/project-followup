namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Domain;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public abstract class TaskStatusChangedProjectionWorkerBase<TAggregateEvent>(
    IProjectProjectionWriter projectionWriter) : ProjectionWorkerBase<TAggregateEvent>
    where TAggregateEvent : IAggregateEvent, IProjectTaskEvent
{
    protected abstract ProjectTaskStatus TargetStatus { get; }

    protected override async Task Materialize(TAggregateEvent domainEvent, CancellationToken cancellationToken)
    {
        var rawProject = await projectionWriter.Get(
            domainEvent.ProjectId,
            cancellationToken);
        if (rawProject is null)
        {
            return;
        }

        var project = rawProject.Value;
        var currentTasks = project.Tasks;
        var existing = currentTasks.SingleOrDefault(t => t.Id == domainEvent.TaskId);
        if (existing.Id == default)
        {
            return;
        }

        var existingIndex = currentTasks.IndexOf(existing);
        existing = existing with { Status = (int)this.TargetStatus };
        currentTasks[existingIndex] = existing;
        project = project with { Tasks = currentTasks };
        await projectionWriter.Update(project, cancellationToken);
    }
}

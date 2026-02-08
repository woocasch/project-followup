namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.EventSourcing;
using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskAddedProjectionWorker(
    IProjectProjectionWriter projectionWriter) : ProjectionWorkerBase<TaskAdded>
{
    protected override async Task Materialize(TaskAdded domainEvent, CancellationToken cancellationToken)
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
        var existingIndex = currentTasks.IndexOf(existing);
        if (existing.Id == Guid.Empty)
        {
            existing = new ProjectRecord.Task(
                domainEvent.TaskId,
                domainEvent.Title,
                domainEvent.DueDate,
                (int)ProjectTaskStatus.Created);
            currentTasks.Add(existing);
        }
        else
        {
            existing = existing with
            {
                Title = domainEvent.Title,
                DueDate = domainEvent.DueDate,
                Status = (int)ProjectTaskStatus.Created,
            };
            currentTasks[existingIndex] = existing;
        }

        project = project with
        {
            Tasks = currentTasks,
        };
        await projectionWriter.Update(project, cancellationToken);
    }
}

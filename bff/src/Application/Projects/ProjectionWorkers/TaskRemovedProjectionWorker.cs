namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskRemovedProjectionWorker(
    IProjectProjectionWriter projectionWriter) : TaskStatusChangedProjectionWorkerBase<TaskRemoved>(projectionWriter)
{
    protected override ProjectTaskStatus TargetStatus => ProjectTaskStatus.Removed;
}

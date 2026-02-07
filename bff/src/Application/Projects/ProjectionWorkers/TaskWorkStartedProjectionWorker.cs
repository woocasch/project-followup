namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskWorkStartedProjectionWorker(
    IProjectProjectionWriter projectionWriter) : TaskStatusChangedProjectionWorkerBase<TaskWorkStarted>(projectionWriter)
{
    protected override ProjectTaskStatus TargetStatus => ProjectTaskStatus.InProgress;
}

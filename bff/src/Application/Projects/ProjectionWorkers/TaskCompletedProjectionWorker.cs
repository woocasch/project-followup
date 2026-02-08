namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskCompletedProjectionWorker(
    IProjectProjectionWriter projectionWriter) : TaskStatusChangedProjectionWorkerBase<TaskCompleted>(projectionWriter)
{
    protected override ProjectTaskStatus TargetStatus => ProjectTaskStatus.Completed;
}

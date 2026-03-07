namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskCompletedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    ILogger<TaskCompletedProjectionWorker> logger)
    : TaskStatusChangedProjectionWorkerBase<TaskCompleted>(projectionWriter, logger)
{
    protected override ProjectTaskStatus TargetStatus => ProjectTaskStatus.Completed;
}

namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskWorkStartedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    ILogger<TaskWorkStartedProjectionWorker> logger)
    : TaskStatusChangedProjectionWorkerBase<TaskWorkStarted>(projectionWriter, logger)
{
    protected override ProjectTaskStatus TargetStatus => ProjectTaskStatus.InProgress;
}

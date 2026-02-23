namespace ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

using System;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskRemovedProjectionWorker(
    IProjectProjectionWriter projectionWriter,
    ILogger<TaskRemovedProjectionWorker> logger)
    : TaskStatusChangedProjectionWorkerBase<TaskRemoved>(projectionWriter, logger)
{
    protected override ProjectTaskStatus TargetStatus => ProjectTaskStatus.Removed;
}

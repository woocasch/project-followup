namespace ProjectFollowUp.BFF.Domain.Project.Events;

public readonly record struct TaskRemoved(
    ProjectId ProjectId,
    Guid TaskId,
    DateTimeOffset RemovedAt) : IAggregateEvent, IProjectTaskEvent;

namespace ProjectFollowUp.BFF.Domain.Project.Events;

public readonly record struct TaskWorkStarted(
    ProjectId ProjectId,
    Guid TaskId,
    DateTimeOffset StartedAt) : IAggregateEvent, IProjectTaskEvent;

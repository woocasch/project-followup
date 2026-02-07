namespace ProjectFollowUp.BFF.Domain.Project.Events;

public readonly record struct TaskCompleted(
    ProjectId ProjectId,
    Guid TaskId,
    DateTimeOffset CompletedAt) : IAggregateEvent;

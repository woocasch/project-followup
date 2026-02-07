namespace ProjectFollowUp.BFF.Domain.Project.Events;

public readonly record struct TaskAdded(
    ProjectId ProjectId,
    Guid TaskId,
    string Title,
    string Description,
    DateTimeOffset CreatedAt) : IAggregateEvent, IProjectTaskEvent;

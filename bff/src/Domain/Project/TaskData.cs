namespace ProjectFollowUp.BFF.Domain.Project;

public readonly record struct TaskData(
    Guid TaskId,
    string Title,
    string Description,
    TaskStatus Status,
    DateTimeOffset CreatedAt);

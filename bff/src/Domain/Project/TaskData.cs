namespace ProjectFollowUp.BFF.Domain.Project;

public readonly record struct TaskData(
    Guid TaskId,
    string Title,
    string Description,
    DateOnly? DueDate,
    ProjectTaskStatus Status,
    DateTimeOffset CreatedAt);

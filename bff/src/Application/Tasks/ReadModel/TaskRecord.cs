namespace ProjectFollowUp.BFF.Application.Tasks.ReadModel;

using ProjectFollowUp.BFF.Domain.Project;

public readonly record struct TaskRecord(
    Guid Id,
    ProjectId ProjectId,
    string Title,
    string Description,
    DateOnly? DueDate,
    ProjectTaskStatus Status,
    DateTimeOffset CreatedAt);

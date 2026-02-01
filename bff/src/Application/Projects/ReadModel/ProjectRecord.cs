namespace ProjectFollowUp.BFF.Application.Projects.ReadModel;

using ProjectFollowUp.BFF.Domain.Project;

public readonly record struct ProjectRecord(
    ProjectId Id,
    string Title,
    string Description,
    DateTimeOffset CreatedAt);

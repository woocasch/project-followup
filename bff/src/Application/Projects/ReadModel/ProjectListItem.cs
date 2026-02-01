namespace ProjectFollowUp.BFF.Application.Projects.ReadModel;

using ProjectFollowUp.BFF.Domain.Project;

public readonly record struct ProjectListItem(
    ProjectId Id,
    string Title,
    string Description);

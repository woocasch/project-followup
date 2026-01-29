namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class UpdateProjectCommand(
    ProjectId projectId,
    string title,
    string description,
    Guid userId,
    DateTimeOffset changedAt) : ICommand
{
    public ProjectId ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public Guid UserId { get; } = userId;

    public DateTimeOffset ChangedAt { get; } = changedAt;
}

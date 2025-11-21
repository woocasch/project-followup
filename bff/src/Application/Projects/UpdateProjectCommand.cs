namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Projects;

public sealed class UpdateProjectCommand(
    ProjectId projectId,
    string title,
    string description,
    Guid userId,
    DateTimeOffset createdAt) : ICommand
{
    public ProjectId ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public Guid UserId { get; } = userId;

    public DateTimeOffset CreatedAt { get; } = createdAt;
}

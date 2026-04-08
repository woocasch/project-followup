namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.User;

public sealed class CreateProjectCommand(
    ProjectId projectId,
    string title,
    string description,
    UserId userId,
    DateTimeOffset createdAt) : ICommand
{
    public ProjectId ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public UserId UserId { get; } = userId;

    public DateTimeOffset CreatedAt { get; } = createdAt;
}

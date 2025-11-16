namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class CreateProjectCommand(
    Guid projectId,
    string title,
    string description,
    Guid userId,
    DateTimeOffset createdAt) : ICommand
{
    public Guid ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public Guid UserId { get; } = userId;

    public DateTimeOffset CreatedAt { get; } = createdAt;
}

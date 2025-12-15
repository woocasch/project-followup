namespace ProjectFollowUp.BFF.Domain.Project.Events;

public readonly struct ProjectDetailsChanged(
    ProjectId projectId,
    string title,
    string description,
    DateTimeOffset createdAt)
{
    public ProjectId ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public DateTimeOffset CreatedAt { get; } = createdAt;
}

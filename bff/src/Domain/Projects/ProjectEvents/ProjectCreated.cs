namespace ProjectFollowUp.BFF.Domain.Projects.ProjectEvents;

public readonly struct ProjectCreated(
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

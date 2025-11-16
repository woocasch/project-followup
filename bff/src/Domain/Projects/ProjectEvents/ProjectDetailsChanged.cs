namespace ProjectFollowUp.BFF.Domain.Projects.ProjectEvents;

public readonly struct ProjectDetailsChanged(
    ProjectId projectId,
    string title,
    string description)
{
    public ProjectId ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;
}

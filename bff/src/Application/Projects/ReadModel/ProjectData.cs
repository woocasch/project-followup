namespace ProjectFollowUp.BFF.Application.Projects.ReadModel;

public sealed class ProjectData(
    Guid id,
    string title,
    string description)
{
    public Guid Id { get; } = id;

    public string Title { get; } = title;

    public string Description { get; } = description;
}

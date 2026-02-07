namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

public sealed class GetOutput(
    Guid id,
    string title,
    string description)
{
    public Guid Id { get; } = id;

    public string Title { get; } = title;

    public string Description { get; } = description;
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

public sealed class CreateInput(
    string title,
    string description)
{
    public string Title { get; } = title;

    public string Description { get; } = description;
}

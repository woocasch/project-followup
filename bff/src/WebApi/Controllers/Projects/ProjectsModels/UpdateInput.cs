namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

public sealed class UpdateInput(
    string title,
    string description)
{
    public string Title { get; } = title;

    public string Description { get; } = description;
}

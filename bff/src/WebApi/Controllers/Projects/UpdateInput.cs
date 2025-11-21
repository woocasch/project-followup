namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public sealed class UpdateInput(
    string title,
    string description)
{
    public string Title { get; } = title;

    public string Description { get; } = description;
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public class UpdateInput(
    string title,
    string description)
{
    public string Title { get; } = title;

    public string Description { get; } = description;
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public class CreateInput
{
    public CreateInput(string title, string description)
    {
        this.Title = title;
        this.Description = description;
    }

    public string Title { get; }

    public string Description { get; }
}

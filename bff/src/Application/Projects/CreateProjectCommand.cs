namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class CreateProjectCommand : ICommand
{
    public CreateProjectCommand(
        Guid projectId,
        string title,
        string description,
        Guid userId)
    {
        this.ProjectId = projectId;
        this.Title = title;
        this.Description = description;
        this.UserId = userId;
    }

    public Guid ProjectId { get; }

    public string Title { get; }

    public string Description { get; }

    public Guid UserId { get; }
}

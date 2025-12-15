namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Domain.Project;

public sealed class GetProjectResult(ProjectId projectId, string title, string description)
{
    public ProjectId ProjectId { get; } = projectId;

    public string Title { get; } = title;

    public string Description { get; } = description;
}

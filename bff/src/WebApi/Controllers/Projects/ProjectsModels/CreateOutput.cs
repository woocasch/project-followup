namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

public sealed class CreateOutput(Guid projectId)
{
    public Guid ProjectId { get; } = projectId;
}

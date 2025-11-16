namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public sealed class CreateOutput(Guid projectId)
{
    public Guid ProjectId { get; } = projectId;
}

namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class GetProjectQuery(Guid userId, ProjectId projectId) : IQuery<GetProjectResult>
{
    public Guid UserId { get; } = userId;

    public ProjectId ProjectId { get; } = projectId;
}

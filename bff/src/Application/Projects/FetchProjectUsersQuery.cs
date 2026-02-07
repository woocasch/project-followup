namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class FetchProjectUsersQuery(
    ProjectId projectId) : IQuery<FetchProjectUsersResult>
{
    public ProjectId ProjectId { get; } = projectId;
}

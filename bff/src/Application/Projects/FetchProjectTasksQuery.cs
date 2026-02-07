namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class FetchProjectTasksQuery(
    ProjectId projectId) : IQuery<FetchProjectTasksResult>
{
    public ProjectId ProjectId { get; } = projectId;
}

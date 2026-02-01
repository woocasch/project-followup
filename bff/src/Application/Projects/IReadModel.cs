namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Projects.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;

public interface IReadModel
{
    Task<IEnumerable<ProjectListItem>> Fetch(CancellationToken cancellationToken);

    Task<ProjectRecord?> Get(ProjectId id, CancellationToken cancellationToken);
}

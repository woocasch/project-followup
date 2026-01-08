namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Application.Projects.ReadModel;

public interface IReadModel
{
    Task<ProjectData?> GetAsync(
        Guid projectId,
        CancellationToken cancellationToken);

    Task<ReadOnlyCollection<ProjectData>> FetchAsync(
        CancellationToken cancellationToken);
}

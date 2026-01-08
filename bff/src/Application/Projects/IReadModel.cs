namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Application.Projects.ReadModel;

public interface IReadModel
{
    Task<ProjectData?> GetAsync(
        Guid projectId,
        CancellationToken cancellationToken);
}

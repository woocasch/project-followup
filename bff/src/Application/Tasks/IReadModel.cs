namespace ProjectFollowUp.BFF.Application.Tasks;

using ProjectFollowUp.BFF.Application.Tasks.ReadModel;
using ProjectFollowUp.BFF.Domain.Project;

public interface IReadModel
{
    Task<IEnumerable<TaskRecord>> Fetch(
        ProjectId projectId,
        CancellationToken cancellationToken);

    Task<TaskRecord?> Get(
        Guid id,
        CancellationToken cancellationToken);
}

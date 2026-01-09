namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class GetProjectQueryHandler(
    IReadModel readModel) : QueryHandlerBase<GetProjectQuery, GetProjectResult>
{
    protected override async Task<GetProjectResult?> HandleQuery(
        GetProjectQuery query,
        CancellationToken cancellationToken)
    {
        var project = await readModel.GetAsync(query.ProjectId.ToGuid(), cancellationToken);
        if (project is null)
        {
            return null;
        }

        var result = new GetProjectResult(
            ProjectId.FromGuid(project.Id),
            project.Title,
            project.Description);
        return result;
    }
}

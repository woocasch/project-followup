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
        var project = await readModel.Get(query.ProjectId, cancellationToken);
        if (project is null)
        {
            return null;
        }

        var result = new GetProjectResult(
            project.Value.Id,
            project.Value.Title,
            project.Value.Description);
        return result;
    }
}

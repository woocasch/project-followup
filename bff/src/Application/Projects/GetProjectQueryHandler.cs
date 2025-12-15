namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class GetProjectQueryHandler : QueryHandlerBase<GetProjectQuery, GetProjectResult>
{
    protected override async Task<GetProjectResult?> HandleQuery(
        GetProjectQuery query,
        CancellationToken cancellationToken)
    {
        await Task.Yield();
        var project = ProjectsStore.GetProjectById(query.ProjectId);
        if (project is null)
        {
            return null;
        }

        var result = new GetProjectResult(
            project.Id,
            project.Title,
            project.Description);
        return result;
    }
}

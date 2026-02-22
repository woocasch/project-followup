namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class GetProjectQueryHandler(
    IReadModel readModel,
    ILogger<GetProjectQueryHandler> logger)
    : QueryHandlerBase<GetProjectQuery, GetProjectResult>(logger)
{
    protected override async Task<GetProjectResult> HandleQuery(
        GetProjectQuery query,
        CancellationToken cancellationToken)
    {
        var project = await readModel.Get(query.ProjectId, cancellationToken);
        if (project is null)
        {
            return GetProjectResult.NotFound();
        }

        var result = new GetProjectResult.ProjectData(
            project.Value.Id,
            project.Value.Title,
            project.Value.Description);
        return GetProjectResult.Success(result);
    }
}

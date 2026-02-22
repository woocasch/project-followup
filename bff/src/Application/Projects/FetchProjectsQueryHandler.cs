namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class FetchProjectsQueryHandler(
    IReadModel readModel,
    ILogger<FetchProjectsQueryHandler> logger)
    : QueryHandlerBase<FetchProjectsQuery, FetchProjectsResult>(logger)
{
    protected override async Task<FetchProjectsResult> HandleQuery(FetchProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await readModel.Fetch(cancellationToken);
        return new FetchProjectsResult(
            projects
            .Select(p => new FetchProjectsResult.Project(
                p.Id,
                p.Title,
                p.Description,
                3,
                4,
                5)));
    }
}

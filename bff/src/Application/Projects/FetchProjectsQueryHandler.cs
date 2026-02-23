namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

public sealed class FetchProjectsQueryHandler(
    IReadModel readModel,
    ILogger<FetchProjectsQueryHandler> logger)
    : QueryHandlerBase<FetchProjectsQuery, FetchProjectsResult>(logger)
{
    protected override async Task<FetchProjectsResult> HandleQuery(FetchProjectsQuery query, CancellationToken cancellationToken)
    {
        logger.Started(query.UserId);
        var projects = await readModel.Fetch(cancellationToken);
        logger.Completed(query.UserId);
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

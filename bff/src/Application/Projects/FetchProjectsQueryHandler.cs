namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class FetchProjectsQueryHandler(
    IReadModel readModel) : QueryHandlerBase<FetchProjectsQuery, FetchProjectsResult>
{
    protected override async Task<FetchProjectsResult?> HandleQuery(FetchProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await readModel.FetchAsync(cancellationToken);
        return new FetchProjectsResult(
            projects
            .Select(p => new FetchProjectsResult.Project(
                ProjectId.FromGuid(p.Id),
                p.Title,
                p.Description,
                3,
                4,
                5)));
    }
}

namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class FetchProjectUsersQueryHandler(
    IReadModel readModel,
    ILogger<FetchProjectUsersQueryHandler> logger)
    : QueryHandlerBase<FetchProjectUsersQuery, FetchProjectUsersResult>(logger)
{
    protected override async Task<FetchProjectUsersResult> HandleQuery(FetchProjectUsersQuery query, CancellationToken cancellationToken)
    {
        var project = await readModel.Get(query.ProjectId, cancellationToken);
        if (project is null || project.Value.AssignedUsers.Count == 0)
        {
            return new FetchProjectUsersResult([]);
        }

        return new FetchProjectUsersResult(
            project.Value.AssignedUsers.Select(u => new FetchProjectUsersResult.User(u.Id, u.DisplayName)));
    }
}

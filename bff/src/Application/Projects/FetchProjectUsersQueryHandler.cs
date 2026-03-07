namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects.ProjectionWorkers;

public sealed class FetchProjectUsersQueryHandler(
    IReadModel readModel,
    ILogger<FetchProjectUsersQueryHandler> logger)
    : QueryHandlerBase<FetchProjectUsersQuery, FetchProjectUsersResult>(logger)
{
    protected override async Task<FetchProjectUsersResult> HandleQuery(FetchProjectUsersQuery query, CancellationToken cancellationToken)
    {
        logger.Started(query.ProjectId.Value);
        var project = await readModel.Get(query.ProjectId, cancellationToken);
        if (project is null)
        {
            logger.ProjectNotFound(query.ProjectId.Value);
            var message = $"Project with id {query.ProjectId.Value} not found.";
            throw new InvalidOperationException(message);
        }

        logger.Completed(query.ProjectId.Value);
        return new FetchProjectUsersResult(
            project.Value.AssignedUsers.Select(u => new FetchProjectUsersResult.User(u.Id, u.DisplayName)));
    }
}

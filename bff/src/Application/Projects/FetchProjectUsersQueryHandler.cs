namespace ProjectFollowUp.BFF.Application.Projects;

using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.User;

public sealed class FetchProjectUsersQueryHandler(
    IReadModel readModel) : QueryHandlerBase<FetchProjectUsersQuery, FetchProjectUsersResult>
{
    protected override async Task<FetchProjectUsersResult?> HandleQuery(FetchProjectUsersQuery query, CancellationToken cancellationToken)
    {
        var project = await readModel.Get(query.ProjectId, cancellationToken);
        if (project is null)
        {
            return new FetchProjectUsersResult([]);
        }

        return new FetchProjectUsersResult(
            project.Value.AssignedUsers.Select(u => new FetchProjectUsersResult.User(u.Id, u.DisplayName)));
    }
}

namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class FetchProjectsQueryHandler : QueryHandlerBase<FetchProjectsQuery, FetchProjectsResult>
{
    protected override async Task<FetchProjectsResult?> HandleQuery(FetchProjectsQuery query, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var projects = new List<FetchProjectsResult.Project>
        {
            new(
                Guid.NewGuid(),
                "Project Alpha",
                "Description for Project Alpha",
                usersCount: 5,
                tasksCompleted: 10,
                tasksTotal: 20),
            new(
                Guid.NewGuid(),
                "Project Beta",
                "Description for Project Beta",
                usersCount: 3,
                tasksCompleted: 7,
                tasksTotal: 15),
        };
        return new FetchProjectsResult(projects);
    }
}

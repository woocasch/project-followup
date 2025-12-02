namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class FetchProjectsQueryHandler : QueryHandlerBase<FetchProjectsQuery, FetchProjectsResult>
{
    protected override async Task<FetchProjectsResult?> HandleQuery(FetchProjectsQuery query, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var projects = GetAllProjects();
        return new FetchProjectsResult(projects);
    }

    private static IEnumerable<FetchProjectsResult.Project> GetAllProjects()
    {
        var projects = UsersStore.GetAllProjects();
        foreach(var project in projects)
        {
            yield return new FetchProjectsResult.Project(
                project.Id,
                project.Title,
                project.Description,
                5,
                7,
                12);
        }
    }
}

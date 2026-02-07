namespace ProjectFollowUp.BFF.Application.Projects;

using System.Threading;
using System.Threading.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;

public sealed class FetchProjectTasksQueryHandler(
    IReadModel readModel) : QueryHandlerBase<FetchProjectTasksQuery, FetchProjectTasksResult>
{
    protected override async Task<FetchProjectTasksResult?> HandleQuery(FetchProjectTasksQuery query, CancellationToken cancellationToken)
    {
        var project = await readModel.Get(query.ProjectId, cancellationToken);
        if (project is null || project.Value.Tasks.Count == 0)
        {
            return new FetchProjectTasksResult([]);
        }

        return new FetchProjectTasksResult(
            project.Value.Tasks.Select(t => new FetchProjectTasksResult.TaskData(t.Id, t.Title, t.Status)));
    }
}

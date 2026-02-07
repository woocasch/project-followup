namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.TasksModels;

[Route("api/projects/{projectId:guid}/tasks")]
[ApiController]
public class TasksController(
    IMediator mediator) : ControllerBase
{
    public const string FetchRouteName = "FetchProjectTasks";

    [HttpGet(Name = FetchRouteName)]
    public async Task<IResult> FetchList(
       Guid projectId,
       CancellationToken cancellationToken)
    {
        var query = new FetchProjectTasksQuery(ProjectId.FromGuid(projectId));
        var result = await mediator.Fetch(query, cancellationToken);

        var tasks = result.Tasks.Select(t => new FetchListOutput.TaskListItem
        {
            Id = t.Id,
            Title = t.Title,
            Status = t.Status.ToString()
        });
        var output = new FetchListOutput([.. tasks]);

        return Results.Ok(output);
    }
}

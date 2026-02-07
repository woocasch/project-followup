namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects.TasksModels;

[Route("api/projects/{projectId:guid}/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    public const string FetchRouteName = "FetchProjectTasks";

    [HttpGet(Name = FetchRouteName)]
    public async Task<IResult> FetchList(
       Guid projectId,
       CancellationToken cancellationToken)
    {
        await Task.Yield();
        if (projectId == Guid.Empty)
        {
            return Results.NotFound();
        }

        return Results.Ok(
            new FetchListOutput(
                [
                    new FetchListOutput.TaskListItem(
                        Guid.NewGuid(),
                        "Investigate hosting possibilities",
                        "In progress"),
                    new FetchListOutput.TaskListItem(
                        Guid.NewGuid(),
                        "Design system architecture",
                        "Completed"),
                ]));
    }
}

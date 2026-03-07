namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Application.Tasks;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.TasksModels;

[Route("api/projects/{projectId:guid}/tasks")]
[ApiController]
public sealed class TasksController(
    IMediator mediator,
    ILogger<TasksController> logger) : ControllerBase
{
    public const string FetchRouteName = "FetchProjectTasks";

    [HttpGet(Name = FetchRouteName)]
    public async Task<IResult> FetchList(
       Guid projectId,
       CancellationToken cancellationToken)
    {
        logger.FetchListStarted(projectId);
        var query = new FetchProjectTasksQuery(ProjectId.FromGuid(projectId));
        var result = await mediator.Fetch(query, cancellationToken);
        logger.FetchListQueryExecuted(projectId);

        var tasks = result.Tasks.Select(t => new FetchListOutput.TaskListItem
        {
            Id = t.Id,
            Title = t.Title,
            DueDate = t.DueDate,
            Status = t.Status
        });
        var output = new FetchListOutput([.. tasks]);
        logger.FetchListResultMapped(projectId, output.Tasks.Length);

        return Results.Ok(output);
    }

    [HttpPost]
    public async Task<IResult> CreateTask(CreateTaskInput input, Guid projectId, CancellationToken cancellationToken)
    {
        logger.CreateTaskStarted(projectId, input.Title);
        var taskId = Guid.NewGuid();
        DateOnly? dueDate = null;
        if (!string.IsNullOrEmpty(input.DueDate))
        {
            if (!DateOnly.TryParse(input.DueDate, out var parsedDate))
            {
                return Results.BadRequest(new { error = "Invalid date format. Expected format: YYYY-MM-DD (e.g., 2025-01-31)." });
            }
            dueDate = parsedDate;
        }

        logger.CreateTaskInputProcessed(projectId, input.Title);
        var command = new CreateTaskCommand(
            ProjectId.FromGuid(projectId),
            taskId,
            input.Title,
            input.Description,
            dueDate);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.CreateTaskCommandFailed(projectId, input.Title, result.ErrorCode, result.Exception);
            return result.ErrorCode switch
            {
                "ProjectNotFound" => Results.Problem("Project not found.", statusCode: 404),
                _ => Results.Problem("Could not create task.")
            };
        }

        logger.CreateTaskCommandSucceeded(projectId, input.Title);
        var output = new CreateTaskOutput(taskId);
        logger.CreateCommandCompleted(projectId, input.Title);
        return Results.Created($"/api/projects/{projectId}/tasks/{taskId}", output);
    }
}

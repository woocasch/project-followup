namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IMediator mediator;

    public ProjectsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public IResult FetchList()
    {
        var projects = new List<ProjectListItem>
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
        var result = new FetchListResult(projects);
        return Results.Ok(result);
    }

    [HttpPost]
    public async Task<IResult> Create(
        CreatePayload payload,
        CancellationToken cancellationToken)
    {
        var projectId = Guid.NewGuid();
        var command = new CreateProjectCommand(
            projectId,
            payload.Title,
            payload.Description,
            // Get user id from token
            Guid.NewGuid());
        var result = await this.mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Results.Problem("Could not create project.");
        }

        return Results.Created();
    }
}

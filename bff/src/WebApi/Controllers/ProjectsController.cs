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
    public async Task<IResult> FetchList(CancellationToken cancellationToken)
    {
        // TODO: Retrieve user id from token.
        var query = new FetchProjectsQuery(Guid.NewGuid());
        var result = await this.mediator.Fetch(query, cancellationToken);
        var projects = result.Projects
            .Select(p => new ProjectListItem(
                p.Id,
                p.Title,
                p.Description,
                p.UsersCount,
                p.TasksCompleted,
                p.TasksTotal))
            .ToList();
        var output = new FetchListOutput(projects);
        return Results.Ok(output);
    }

    [HttpPost]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        var projectId = Guid.NewGuid();
        var command = new CreateProjectCommand(
            projectId,
            payload.Title,
            payload.Description,
            // TODO: Get user id from token
            Guid.NewGuid());
        var result = await this.mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Results.Problem("Could not create project.");
        }

        return Results.Created();
    }
}

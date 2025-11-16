namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Domain.Projects;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects;

[Route("api/[controller]")]
[ApiController]
public sealed class ProjectsController(IMediator mediator) : ControllerBase
{
    public const string GetRouteName = "GetProjectById";

    [HttpGet]
    public async Task<IResult> FetchList(CancellationToken cancellationToken)
    {
        // TODO: Retrieve user id from token.
        var query = new FetchProjectsQuery(Guid.NewGuid());
        var result = await mediator.Fetch(query, cancellationToken);
        var projects = result.Projects
            .Select(p => new ProjectListItem(
                p.Id.ToGuid(),
                p.Title,
                p.Description,
                p.UsersCount,
                p.TasksCompleted,
                p.TasksTotal))
            .ToList();
        var output = new FetchListOutput([.. projects]);
        return Results.Ok(output);
    }

    [HttpGet("{projectId:guid}", Name = GetRouteName)]
    public async Task<IResult> Get(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var query = new GetProjectQuery(Guid.NewGuid(), ProjectId.FromGuid(projectId));
        var result = await mediator.Fetch(query, cancellationToken);
        var output = new GetOutput(
            result.ProjectId.ToGuid(),
            result.Title,
            result.Description);
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
            Guid.NewGuid(),
            DateTimeOffset.UtcNow);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Results.Problem("Could not create project.");
        }

        return Results.CreatedAtRoute(GetRouteName, new { projectId }, new CreateOutput(projectId));
    }
}

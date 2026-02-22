namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

[Route("api/[controller]")]
[ApiController]
public sealed class ProjectsController(IMediator mediator) : ControllerBase
{
    public const string GetRouteName = "GetProjectById";

    [HttpGet]
    public async Task<IResult> FetchList(CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var query = new FetchProjectsQuery(userId);
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
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var query = new GetProjectQuery(userId, ProjectId.FromGuid(projectId));
        var result = await mediator.Fetch(query, cancellationToken);
        if (!result.ProjectExists)
        {
            return Results.NotFound();
        }

        var project = result.Project!;
        var output = new GetOutput(
            project.ProjectId.ToGuid(),
            project.Title,
            project.Description);
        return Results.Ok(output);
    }

    [HttpPost]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var projectId = Guid.NewGuid();
        var command = new CreateProjectCommand(
            ProjectId.FromGuid(projectId),
            payload.Title,
            payload.Description,
            userId,
            DateTimeOffset.UtcNow);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Results.Problem("Could not create project.");
        }

        return Results.CreatedAtRoute(GetRouteName, new { projectId }, new CreateOutput(projectId));
    }

    [HttpPut("{projectId:guid}")]
    public async Task<IResult> Update(
        Guid projectId,
        UpdateInput payload,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var command = new UpdateProjectCommand(
            ProjectId.FromGuid(projectId),
            payload.Title,
            payload.Description,
            userId,
            DateTimeOffset.UtcNow);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Results.Problem("Could not update project.");
        }

        return Results.AcceptedAtRoute(GetRouteName, new { projectId });
    }
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

[Route("api/[controller]")]
[ApiController]
public sealed class ProjectsController(
    IMediator mediator,
    ILogger<ProjectsController> logger) : ControllerBase
{
    public const string GetRouteName = "GetProjectById";

    [HttpGet]
    public async Task<IResult> FetchList(CancellationToken cancellationToken)
    {
        logger.FetchListStarted();
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            logger.FetchListUserNotAuthorized();
            return Results.Unauthorized();
        }

        var query = new FetchProjectsQuery(userId);
        logger.FetchListFetchingData();
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
        logger.FetchListCompleted();
        return Results.Ok(output);
    }

    [HttpGet("{projectId:guid}", Name = GetRouteName)]
    public async Task<IResult> Get(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        logger.GetStarted();
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            logger.GetUserNotAuthorized();
            return Results.Unauthorized();
        }

        var query = new GetProjectQuery(userId, ProjectId.FromGuid(projectId));
        logger.GetFetchingData();
        var result = await mediator.Fetch(query, cancellationToken);
        if (!result.ProjectExists)
        {
            logger.GetProjectNotFound(projectId);
            return Results.NotFound();
        }

        var project = result.Project!;
        var output = new GetOutput(
            project.ProjectId.ToGuid(),
            project.Title,
            project.Description);
        logger.GetCompleted();
        return Results.Ok(output);
    }

    [HttpPost]
    [Authorize(Roles = "manage-projects")]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        logger.CreateStarted();
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            logger.CreateUserNotAuthorized();
            return Results.Unauthorized();
        }

        var projectId = Guid.NewGuid();
        logger.CreateCreatingProject();
        var command = new CreateProjectCommand(
            ProjectId.FromGuid(projectId),
            payload.Title,
            payload.Description,
            userId,
            DateTimeOffset.UtcNow);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.CreateCreationFailed();
            return Results.Problem("Could not create project.");
        }

        logger.CreateCompleted(projectId);
        return Results.CreatedAtRoute(GetRouteName, new { projectId }, new CreateOutput(projectId));
    }

    [HttpPut("{projectId:guid}")]
    public async Task<IResult> Update(
        Guid projectId,
        UpdateInput payload,
        CancellationToken cancellationToken)
    {
        logger.UpdateStarted();
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            logger.UpdateUserNotAuthorized();
            return Results.Unauthorized();
        }

        logger.UpdateUpdatingProject(projectId);
        var command = new UpdateProjectCommand(
            ProjectId.FromGuid(projectId),
            payload.Title,
            payload.Description,
            userId,
            DateTimeOffset.UtcNow);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.UpdateUpdateFailed(projectId);
            return Results.Problem("Could not update project.");
        }

        logger.UpdateCompleted(projectId);
        return Results.AcceptedAtRoute(GetRouteName, new { projectId });
    }
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.UsersModels;

[Route("api/projects/{projectId:guid}/users")]
[ApiController]
public class UsersController(
    IMediator mediator,
    ILogger<UsersController> logger) : ControllerBase
{
    public const string FetchRouteName = "FetchProjectUsers";

    [HttpGet(Name = FetchRouteName)]
    public async Task<IResult> FetchList(
       Guid projectId,
       CancellationToken cancellationToken)
    {
        logger.FetchListStarted(projectId);
        var query = new FetchProjectUsersQuery(ProjectId.FromGuid(projectId));
        var result = await mediator.Fetch(query, cancellationToken);
        logger.FetchListDataRetrieved(projectId, result.Users.Count);

        var users = result.Users
            .Select(u => new FetchListOutput.UserListItem(
                u.Id.ToGuid(),
                u.DisplayName))
            .ToList();
        logger.FetchListResultMapped(projectId, users.Count);
        var output = new FetchListOutput([.. users]);
        logger.FetchListCompleted(projectId, users.Count);
        return Results.Ok(output);
    }
}

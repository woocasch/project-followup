namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Projects;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects.UsersModels;

[Route("api/projects/{projectId:guid}/users")]
[ApiController]
public class UsersController(
    IMediator mediator) : ControllerBase
{
    public const string FetchRouteName = "FetchProjectUsers";

    [HttpGet(Name = FetchRouteName)]
    public async Task<IResult> FetchList(
       Guid projectId,
       CancellationToken cancellationToken)
    {
        var query = new FetchProjectUsersQuery(ProjectId.FromGuid(projectId));
        var result = await mediator.Fetch(query, cancellationToken);
        
        var users = result.Users
            .Select(u => new FetchListOutput.UserListItem(
                u.Id.ToGuid(),
                u.DisplayName))
            .ToList();
        var output = new FetchListOutput([.. users]);
        return Results.Ok(output);
    }
}

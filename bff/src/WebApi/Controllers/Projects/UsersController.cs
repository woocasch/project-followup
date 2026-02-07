namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.WebApi.Controllers.Projects.UsersModels;

[Route("api/projects/{projectId:guid}/users")]
[ApiController]
public class UsersController : ControllerBase
{
    public const string FetchRouteName = "FetchProjectUsers";

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
                    new FetchListOutput.UserListItem(
                        Guid.NewGuid(),
                        "John Doe"),
                    new FetchListOutput.UserListItem(
                        Guid.NewGuid(),
                        "Jane Doe")
                ]));
    }
}

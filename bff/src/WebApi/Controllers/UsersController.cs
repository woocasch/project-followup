namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.WebApi.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        var projectId = Guid.NewGuid();
        var success = Random.Shared.Next(0, 10) > 3;
        if (!success)
        {
            return Results.Problem("Could not create user.");
        }

        return Results.Created();
    }
}

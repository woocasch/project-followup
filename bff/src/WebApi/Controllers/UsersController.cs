namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.Domain.Users;
using ProjectFollowUp.BFF.WebApi.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var command = new CreateUserCommand(
            UserId.FromGuid(userId),
            payload.Email,
            payload.DisplayName);
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return Results.Problem("Could not create user.");
        }

        return Results.Created();
    }
}

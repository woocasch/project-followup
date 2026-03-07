namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Application.Users;
using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.WebApi.Controllers.Users.UsersModels;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController(
    IMediator mediator,
    ILogger<UsersController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        logger.CreateStarted(payload.Email);
        var userId = Guid.NewGuid();
        var command = new CreateUserCommand(
            UserId.FromGuid(userId),
            payload.DisplayName,
            payload.Email);
        var result = await mediator.Send(command, cancellationToken);
        logger.CreateCommandExecuted(payload.Email);
        if (!result.IsSuccess)
        {
            logger.CreateCommandFailed(payload.Email, result.ErrorCode, result.Exception);
            return Results.Problem("Could not create user.");
        }

        logger.CreateCompleted(payload.Email);
        return Results.Created();
    }
}

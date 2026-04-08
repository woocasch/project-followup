namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = Roles.UserManagement.CreateUser)]
    public async Task<IResult> Create(
        CreateInput payload,
        CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        logger.CreateStarted(userId);
        var command = new CreateUserCommand(
            UserId.FromGuid(userId),
            payload.DisplayName,
            payload.Email);
        var result = await mediator.Send(command, cancellationToken);
        logger.CreateCommandExecuted(userId);
        if (!result.IsSuccess)
        {
            logger.CreateCommandFailed(userId, result.ErrorCode, result.Exception);
            return Results.Problem("Could not create user.");
        }

        logger.CreateCompleted(userId);
        return Results.Created();
    }
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.WebApi.Controllers.Users.LinkCodesModels;

[Route("api/users/linkCodes")]
[ApiController]
public sealed class LinkCodesController(
    IMediator mediator,
    ILogger<LinkCodesController> logger) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{linkCode}")]
    public async Task<IActionResult> GetByLinkCode(string linkCode, CancellationToken cancellationToken)
    {
        logger.GetByLinkCodeStarted(linkCode);
        if (string.IsNullOrEmpty(linkCode))
        {
            return NotFound();
        }

        var query = GetActivationLinkDataQuery.ByLinkCode(linkCode);
        var result = await mediator.Fetch(query, cancellationToken);
        logger.GetByLinkCodeDataRetrieved(linkCode);
        if (!result.LinkFound)
        {
            logger.GetByLinkCodeLinkCodeNotFound(linkCode);
            return NotFound();
        }

        var link = result.Link!;
        logger.GetByLinkCodeCompleted(linkCode);
        return Ok(new GetByLinkCodeOutput(
            link.EmailAddress,
            link.DisplayName,
            link.IsUsed));
    }
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.WebApi.Controllers.Users.LinkCodesModels;

[Route("api/users/linkCodes")]
[ApiController]
public class LinkCodesController(
    IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{linkCode}")]
    public async Task<IActionResult> GetByLinkCode(string linkCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(linkCode))
        {
            return NotFound();
        }

        var query = GetActivationLinkDataQuery.ByLinkCode(linkCode);
        var result = await mediator.Fetch(query, cancellationToken);
        if (!result.LinkFound)
        {
            return NotFound();
        }

        var link = result.Link!;
        return Ok(new GetByLinkCodeOutput(
            link.EmailAddress,
            link.DisplayName,
            link.IsUsed));
    }
}

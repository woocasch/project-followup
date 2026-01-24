namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProjectFollowUp.BFF.Application.ActivationLinks;
using ProjectFollowUp.BFF.Application.Cqrs;

[Route("api/[controller]")]
[ApiController]
public class AccountsController(
    IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("linkCodes/{linkCode}")]
    public async Task<IActionResult> GetByLinkCode(string linkCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(linkCode))
        {
            return NotFound();
        }

        var query = new GetActivationLinkDataQuery(linkCode);
        var result = await mediator.Fetch(query, cancellationToken);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(new
        {
            EmailAddress = result.EmailAddress,
            DisplayName = result.DisplayName,
            IsUsed = result.IsUsed,
        });
    }
}

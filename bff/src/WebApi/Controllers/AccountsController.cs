namespace ProjectFollowUp.BFF.WebApi.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("linkCodes/{linkCode}")]
    public IActionResult GetByLinkCode(string linkCode)
    {
        // Placeholder implementation
        return Ok(new
        {
            EmailAddress = "test@domain.com",
            DisplayName = "Test 6",
            IsUsed = false
        });
    }
}

namespace ProjectFollowUp.BFF.WebApi;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

public static class ControllerBaseExtensions
{
    public static Guid? GetUserId(
        this ControllerBase controller)
    {
        var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier) ?? controller.User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return null;
        }

        return userId;
    }
}

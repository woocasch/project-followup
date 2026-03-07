namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public sealed class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string AuthenticationScheme = "TestScheme";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var authHeaderValue = authHeader.ToString();
        if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var token = authHeaderValue["Bearer ".Length..].Trim();

        try
        {
            var testToken = JsonSerializer.Deserialize<TestToken>(token);
            if (testToken is null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token format"));
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, testToken.UserId),
                new("sub", testToken.UserId),
                new(ClaimTypes.Name, testToken.UserName)
            };

            if (testToken.Claims is not null)
            {
                foreach (var claim in testToken.Claims)
                {
                    claims.Add(new Claim(claim.Type, claim.Value));
                }
            }

            var identity = new ClaimsIdentity(claims, AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (JsonException)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid token format"));
        }
    }
}

public sealed record TestToken(
    string UserId,
    string UserName,
    List<TestClaim>? Claims = null);

public sealed record TestClaim(string Type, string Value);

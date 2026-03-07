namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Text.Json;

public static class TestAuthTokenGenerator
{
    public static string GenerateToken(
        string userId,
        string userName,
        params (string Type, string Value)[] claims)
    {
        var testClaims = claims.Select(c => new TestClaim(c.Type, c.Value)).ToList();
        var token = new TestToken(userId, userName, testClaims.Count > 0 ? testClaims : null);
        return JsonSerializer.Serialize(token);
    }

    public static string GenerateToken(Guid userId, string userName, params (string Type, string Value)[] claims)
    {
        return GenerateToken(userId.ToString(), userName, claims);
    }
}

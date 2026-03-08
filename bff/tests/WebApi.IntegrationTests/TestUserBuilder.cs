namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Collections.ObjectModel;
using System.Security.Claims;

/// <summary>
/// Builder for creating test authentication tokens with a fluent API
/// </summary>
public sealed class TestUserBuilder
{
    private Guid userId = Guid.NewGuid();

    private string userName = "test.user@example.com";

    private readonly List<(string Type, string Value)> claims = [];

    public TestUserBuilder WithUserId(Guid userId)
    {
        this.userId = userId;
        return this;
    }

    public TestUserBuilder WithUserName(string userName)
    {
        this.userName = userName;
        return this;
    }

    public TestUserBuilder WithEmail(string email)
    {
        this.claims.Add((ClaimTypes.Email, email));
        return this;
    }

    public TestUserBuilder WithRole(string role)
    {
        this.claims.Add((ClaimTypes.Role, role));
        return this;
    }

    public TestUserBuilder WithClaim(string type, string value)
    {
        this.claims.Add((type, value));
        return this;
    }

    public TestUser BuildUser()
    {
        return new(this.userId, this.userName, [.. this.claims]);
    }

    public string BuildToken()
    {
        return TestAuthTokenGenerator.GenerateToken(this.userId, this.userName, [.. this.claims]);
    }

    public HttpClient ApplyTo(HttpClient client)
    {
        return client.WithTestUser(this.userId, this.userName, [.. this.claims]);
    }

    public static TestUserBuilder Create() => new();

    public static TestUserBuilder CreateAdmin(string adminName = "admin@example.com")
    {
        return new TestUserBuilder()
            .WithUserName(adminName)
            .WithEmail(adminName)
            .WithRole("admin");
    }

    public static TestUserBuilder CreateUser(string userName = "user@example.com")
    {
        return new TestUserBuilder()
            .WithUserName(userName)
            .WithEmail(userName)
            .WithRole("user");
    }

    public record class TestUser(
        Guid UserId,
        string UserName,
        ReadOnlyCollection<(string Type, string Value)> Claims);
}

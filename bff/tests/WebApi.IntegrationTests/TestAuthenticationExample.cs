namespace ProjectFollowUp.BFF.WebApi.IntegrationTests;

using System.Net;
using System.Net.Http.Json;

/// <summary>
/// Example test demonstrating how to use the test authentication system
/// </summary>
public sealed class TestAuthenticationExample(WebApiFactory factory)
    : TestBase(factory)
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task Example_UnauthenticatedRequest_ReturnsUnauthorized()
    {
        // Arrange - no authentication header

        // Act
        var response = await this.client.GetAsync("/api/projects");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Example_AuthenticatedRequest_WithUserId_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userName = "test.user@example.com";

        this.client.WithTestUser(userId, userName);

        // Act
        var response = await this.client.GetAsync("/api/projects");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Example_AuthenticatedRequest_WithCustomClaims_ReturnsOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userName = "admin.user@example.com";

        this.client.WithTestUser(
            userId,
            userName,
            ("role", "admin"),
            ("email", "admin.user@example.com"),
            ("department", "IT"));

        // Act
        var response = await this.client.GetAsync("/api/projects");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Example_UsingBuilder_WithAdminUser()
    {
        // Arrange
        TestUserBuilder
            .CreateAdmin("super.admin@example.com")
            .WithUserId(Guid.NewGuid())
            .WithClaim("department", "IT")
            .WithClaim("level", "senior")
            .ApplyTo(this.client);

        // Act
        var response = await this.client.GetAsync("/api/projects");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Example_UsingBuilder_WithCustomUser()
    {
        // Arrange
        var userId = Guid.NewGuid();

        TestUserBuilder
            .Create()
            .WithUserId(userId)
            .WithUserName("project.manager@example.com")
            .WithEmail("project.manager@example.com")
            .WithRole("project-manager")
            .WithClaim("team", "backend")
            .ApplyTo(this.client);

        // Act
        var response = await this.client.GetAsync("/api/projects");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}

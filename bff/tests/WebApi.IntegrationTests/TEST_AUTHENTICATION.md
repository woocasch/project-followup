# Test Authentication Guide

This guide explains how to use the mock authentication system for integration tests.

## Overview

The test authentication system replaces the real Keycloak authentication with a lightweight mock that accepts JSON tokens containing user identity and claims. This eliminates the need to spin up Keycloak in a test container, significantly speeding up test execution.

## Components

### 1. TestAuthHandler
A custom authentication handler that processes JSON-serialized tokens from the `Authorization` header.

### 2. TestAuthTokenGenerator
A utility class for generating test tokens with user identity and claims.

### 3. HttpClientExtensions
Extension methods to easily configure authenticated requests in tests.

### 4. WebApiFactory
Configured to use `TestAuthHandler` instead of JWT Bearer authentication for integration tests.

## Usage

### Basic Authentication (Default User)

All tests inheriting from `TestBase` automatically use a default test user:
- **User ID**: `00000000-0000-0000-0000-000000000001`
- **User Name**: `test.user@example.com`

```csharp
public sealed class MyTests(WebApiFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task MyTest()
    {
        // Client is already authenticated with default user
        var response = await this.Client.GetAsync("/api/projects");
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
```

### Custom User Authentication

Override the default user for specific tests:

```csharp
public sealed class MyTests(WebApiFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task TestWithCustomUser()
    {
        // Arrange
        var customUserId = Guid.NewGuid();
        var customUserName = "custom.user@example.com";
        
        this.Client.WithTestUser(customUserId, customUserName);
        
        // Act
        var response = await this.Client.GetAsync("/api/projects");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
```

### Authentication with Custom Claims

Add custom claims for tests requiring specific roles or attributes:

```csharp
public sealed class MyTests(WebApiFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task TestWithAdminRole()
    {
        // Arrange
        var adminUserId = Guid.NewGuid();

        this.Client.WithTestUser(
            adminUserId,
            "admin@example.com",
            ("role", "admin"),
            ("department", "IT"),
            ("email", "admin@example.com"));

        // Act
        var response = await this.Client.GetAsync("/api/admin/users");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
```

### Using TestUserBuilder (Fluent API)

For more complex scenarios, use the builder pattern:

```csharp
public sealed class MyTests(WebApiFactory factory) : TestBase(factory)
{
    [Fact]
    public async Task TestWithBuilderPattern()
    {
        // Arrange
        TestUserBuilder
            .CreateAdmin("super.admin@example.com")
            .WithUserId(Guid.NewGuid())
            .WithClaim("department", "IT")
            .WithClaim("level", "senior")
            .ApplyTo(this.Client);

        // Act
        var response = await this.Client.GetAsync("/api/admin/users");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task TestWithCustomBuilder()
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
            .WithClaim("location", "US")
            .ApplyTo(this.Client);

        // Act
        var response = await this.Client.GetAsync("/api/projects");

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}
```

### Testing Unauthorized Access

Create a client without authentication:

```csharp
public sealed class MyTests(WebApiFactory factory)
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task TestUnauthorizedAccess()
    {
        // Arrange - no authentication header
        
        // Act
        var response = await this.client.GetAsync("/api/projects");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
```

## Token Format

The test authentication system uses JSON tokens with the following structure:

```json
{
  "UserId": "123e4567-e89b-12d3-a456-426614174000",
  "UserName": "test.user@example.com",
  "Claims": [
    {
      "Type": "role",
      "Value": "admin"
    },
    {
      "Type": "email",
      "Value": "test.user@example.com"
    }
  ]
}
```

## How It Works

1. The `WebApiFactory` configures `TestAuthHandler` as the authentication scheme for integration tests
2. When a request includes an `Authorization: Bearer <token>` header, the handler deserializes the JSON token
3. The handler creates a `ClaimsPrincipal` with the specified user identity and claims
4. Standard claims are automatically added:
   - `ClaimTypes.NameIdentifier`: Set to the `UserId`
   - `sub`: Set to the `UserId` (compatible with JWT standard)
   - `ClaimTypes.Name`: Set to the `UserName`

## Benefits

- ✅ **Fast**: No need to start Keycloak container (~30-60 seconds saved per test run)
- ✅ **Simple**: Easy to create users with specific identities and claims
- ✅ **Flexible**: Full control over user attributes for each test
- ✅ **Isolated**: Each test can use different users without interference
- ✅ **Predictable**: No external dependencies or network calls

## Example Test

See `TestAuthenticationExample.cs` for complete examples of all authentication scenarios.

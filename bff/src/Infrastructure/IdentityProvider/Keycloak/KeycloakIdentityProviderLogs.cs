namespace ProjectFollowUp.BFF.Infrastructure.IdentityProvider.Keycloak;

using Microsoft.Extensions.Logging;

internal static partial class KeycloakIdentityProviderLogs
{
    [LoggerMessage(
        EventId = EventIds.CreateUserStarted,
        EventName = nameof(EventIds.CreateUserStarted),
        Level = LogLevel.Debug,
        Message = "Started creating user '{Email}'.")]
    public static partial void CreateUserStarted(
        this ILogger<KeycloakIdentityProvider> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.CreateUserCreationFailed,
        EventName = nameof(EventIds.CreateUserCreationFailed),
        Level = LogLevel.Error,
        Message = "Failed to create user '{Email}'.")]
    public static partial void CreateUserCreationFailed(
        this ILogger<KeycloakIdentityProvider> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.CreateUserSearchingForUser,
        EventName = nameof(EventIds.CreateUserSearchingForUser),
        Level = LogLevel.Trace,
        Message = "Searching for user '{Email}' after creation.")]
    public static partial void CreateUserSearchingForUser(
        this ILogger<KeycloakIdentityProvider> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.CreateUserUserNotFound,
        EventName = nameof(EventIds.CreateUserUserNotFound),
        Level = LogLevel.Error,
        Message = "User '{Email}' not found after creation.")]
    public static partial void CreateUserUserNotFound(
        this ILogger<KeycloakIdentityProvider> logger,
        string email);

    [LoggerMessage(
        EventId = EventIds.CreateUserCompleted,
        EventName = nameof(EventIds.CreateUserCompleted),
        Level = LogLevel.Debug,
        Message = "Completed creating user '{Email}' with ID '{UserId}'.")]
    public static partial void CreateUserCompleted(
        this ILogger<KeycloakIdentityProvider> logger,
        string email,
        Guid userId);

    private static class EventIds
    {
        public const int CreateUserStarted = 1;

        public const int CreateUserCreationFailed = 2;

        public const int CreateUserSearchingForUser = 3;

        public const int CreateUserUserNotFound = 4;

        public const int CreateUserCompleted = 5;
    }
}

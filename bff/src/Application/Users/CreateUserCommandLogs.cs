namespace ProjectFollowUp.BFF.Application.Users;

using Microsoft.Extensions.Logging;

internal static partial class CreateUserCommandLogs
{
    [LoggerMessage(
        EventId = EventIds.Started,
        EventName = nameof(EventIds.Started),
        Level = LogLevel.Trace,
        Message = "Started creating user with id {UserId}.")]
    public static partial void Started(
        this ILogger<CreateUserCommandHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.CredentialsCreationFailed,
        EventName = nameof(EventIds.CredentialsCreationFailed),
        Level = LogLevel.Error,
        Message = "Failed to create credentials for user with id {UserId}.")]
    public static partial void CredentialsCreationFailed(
        this ILogger<CreateUserCommandHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.CreatingUserProfile,
        EventName = nameof(EventIds.CreatingUserProfile),
        Level = LogLevel.Debug,
        Message = "Creating user profile for user with id {UserId}.")]
    public static partial void CreatingUserProfile(
        this ILogger<CreateUserCommandHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.PublishingUserRegisteredEvent,
        EventName = nameof(EventIds.PublishingUserRegisteredEvent),
        Level = LogLevel.Debug,
        Message = "Publishing UserRegistered event for user with id {UserId}.")]
    public static partial void PublishingUserRegisteredEvent(
        this ILogger<CreateUserCommandHandler> logger,
        Guid userId);

    [LoggerMessage(
        EventId = EventIds.Completed,
        EventName = nameof(EventIds.Completed),
        Level = LogLevel.Trace,
        Message = "Completed creating user with id {UserId}.")]
    public static partial void Completed(
        this ILogger<CreateUserCommandHandler> logger,
        Guid userId);

    private static class EventIds
    {
        public const int Started = 1;

        public const int CredentialsCreationFailed = 2;

        public const int CreatingUserProfile = 3;

        public const int PublishingUserRegisteredEvent = 4;

        public const int Completed = 5;
    }
}
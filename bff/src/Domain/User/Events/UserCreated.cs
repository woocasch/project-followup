namespace ProjectFollowUp.BFF.Domain.User.Events;

public record struct UserCreated(
    UserId UserId,
    Guid CredentialsId,
    string DisplayName,
    EmailAddress Email,
    DateTimeOffset CreatedAt) : IAggregateEvent;

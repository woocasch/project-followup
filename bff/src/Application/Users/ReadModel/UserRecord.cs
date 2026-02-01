namespace ProjectFollowUp.BFF.Application.Users.ReadModel;

using ProjectFollowUp.BFF.Domain.User;

public readonly record struct UserRecord(
    UserId Id,
    Guid CredentialsId,
    EmailAddress Email,
    string DisplayName,
    DateTimeOffset CreatedAt);

namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.ReadModel.User;

public sealed record UserRecord(
    Guid Id,
    Guid CredentialsId,
    string Email,
    string? DisplayName,
    DateTimeOffset CreatedAt);

namespace ProjectFollowUp.BFF.Domain.Users.UserEvents;

public readonly struct UserCreated(
    UserId id,
    string displayName,
    string email,
    DateTimeOffset createdAt)
{
    public UserId Id { get; } = id;

    public string DisplayName { get; } = displayName;

    public string Email { get; } = email;

    public DateTimeOffset CreatedAt { get; } = createdAt;
}

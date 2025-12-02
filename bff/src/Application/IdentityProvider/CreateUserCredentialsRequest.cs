namespace ProjectFollowUp.BFF.Application.IdentityProvider;

using ProjectFollowUp.BFF.Domain.Users;

public sealed class CreateUserCredentialsRequest(
    UserId userId,
    string email,
    string displayName)
{
    public UserId UserId { get; } = userId;

    public string Email { get; } = email;

    public string DisplayName { get; } = displayName;
}

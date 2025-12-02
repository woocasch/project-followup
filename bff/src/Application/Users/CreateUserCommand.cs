namespace ProjectFollowUp.BFF.Application.Users;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Users;

public sealed class CreateUserCommand(
    UserId id,
    string displayName,
    string email) : ICommand
{
    public UserId Id { get; } = id;

    public string DisplayName { get; } = displayName;

    public string Email { get; } = email;
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Users;

public sealed class CreateInput(
    string email,
    string displayName)
{
    public string Email { get; } = email;

    public string DisplayName { get; } = displayName;
}

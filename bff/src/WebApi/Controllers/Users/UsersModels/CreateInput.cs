namespace ProjectFollowUp.BFF.WebApi.Controllers.Users.UsersModels;

public sealed class CreateInput(
    string email,
    string displayName)
{
    public string Email { get; } = email;

    public string DisplayName { get; } = displayName;
}

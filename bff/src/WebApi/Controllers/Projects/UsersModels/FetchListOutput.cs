namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.UsersModels;

public readonly record struct FetchListOutput(
    FetchListOutput.UserListItem[] Users)
{
    public readonly record struct UserListItem(
        Guid Id,
        string DisplayName);
}

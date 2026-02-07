namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.User;

public sealed class FetchProjectUsersResult(
    IEnumerable<FetchProjectUsersResult.User> users)
{
    public ReadOnlyCollection<User> Users { get; } = new([.. users]);

    public sealed class User(
        UserId id,
        string displayName)
    {
        public UserId Id { get; } = id;

        public string DisplayName { get; } = displayName;
    }
}

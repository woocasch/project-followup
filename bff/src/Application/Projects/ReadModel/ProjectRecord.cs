namespace ProjectFollowUp.BFF.Application.Projects.ReadModel;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.User;

public readonly record struct ProjectRecord(
    ProjectId Id,
    string Title,
    string Description,
    DateTimeOffset CreatedAt,
    Collection<ProjectRecord.AssignedUser> AssignedUsers)
{
    public readonly record struct AssignedUser(
        UserId Id,
        string DisplayName);
}

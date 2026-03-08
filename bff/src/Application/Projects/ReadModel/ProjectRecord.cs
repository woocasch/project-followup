namespace ProjectFollowUp.BFF.Application.Projects.ReadModel;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.User;

public readonly record struct ProjectRecord(
    ProjectId Id,
    string Title,
    string Description,
    UserId CreatedBy,
    DateTimeOffset CreatedAt,
    Collection<ProjectRecord.AssignedUser> AssignedUsers,
    Collection<ProjectRecord.Task> Tasks)
{
    public readonly record struct AssignedUser(
        UserId Id,
        string DisplayName,
        string Role);

    public readonly record struct Task(
        Guid Id,
        string Title,
        DateOnly? DueDate,
        ProjectTaskStatus Status);
}

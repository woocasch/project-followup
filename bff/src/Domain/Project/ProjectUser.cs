namespace ProjectFollowUp.BFF.Domain.Project;

using ProjectFollowUp.BFF.Domain.User;

public readonly record struct ProjectUser(
    UserId UserId,
    ProjectUser.RoleInProject Role)
{
    public enum RoleInProject
    {
        Owner,
        Administrator,
        User
    }
};
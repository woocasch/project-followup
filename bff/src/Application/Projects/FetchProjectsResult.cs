namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Project;

public sealed class FetchProjectsResult(IEnumerable<FetchProjectsResult.Project> projects)
{
    public ReadOnlyCollection<Project> Projects { get; } = new ReadOnlyCollection<Project>([.. projects]);

    public sealed class Project(
        ProjectId id,
        string title,
        string description,
        int usersCount,
        int tasksCompleted,
        int tasksTotal)
    {
        public ProjectId Id { get; } = id;

        public string Title { get; } = title;

        public string Description { get; } = description;

        public int UsersCount { get; } = usersCount;

        public int TasksCompleted { get; } = tasksCompleted;

        public int TasksTotal { get; } = tasksTotal;
    }
}

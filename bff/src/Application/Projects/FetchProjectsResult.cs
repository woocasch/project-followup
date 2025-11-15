namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

public sealed class FetchProjectsResult
{
    public FetchProjectsResult(IEnumerable<Project> projects)
    {
        this.Projects = new ReadOnlyCollection<Project>(projects.ToList());
    }

    public ReadOnlyCollection<Project> Projects { get; }

    public sealed class Project
    {
        public Project(
            Guid id,
            string title,
            string description,
            int usersCount,
            int tasksCompleted,
            int tasksTotal)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.UsersCount = usersCount;
            this.TasksCompleted = tasksCompleted;
            this.TasksTotal = tasksTotal;
        }

        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public int UsersCount { get; }

        public int TasksCompleted { get; }

        public int TasksTotal { get; }
    }
}

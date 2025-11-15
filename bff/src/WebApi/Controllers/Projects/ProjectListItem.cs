namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public class ProjectListItem
{
    public ProjectListItem(
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

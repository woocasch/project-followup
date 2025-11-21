namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

public sealed class ProjectListItem(
    Guid id,
    string title,
    string description,
    int usersCount,
    int tasksCompleted,
    int tasksTotal)
{
    public Guid Id { get; } = id;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public int UsersCount { get; } = usersCount;

    public int TasksCompleted { get; } = tasksCompleted;

    public int TasksTotal { get; } = tasksTotal;
}

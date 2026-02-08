namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Project;

public sealed class FetchProjectTasksResult(
    IEnumerable<FetchProjectTasksResult.TaskData> tasks)
{
    public ReadOnlyCollection<TaskData> Tasks { get; } = new([.. tasks]);

    public sealed class TaskData(
        Guid id,
        string title,
        DateOnly? dueDate,
        ProjectTaskStatus status)
    {
        public Guid Id { get; } = id;

        public string Title { get; } = title;

        public DateOnly? DueDate { get; } = dueDate;

        public ProjectTaskStatus Status { get; } = status;
    }
}

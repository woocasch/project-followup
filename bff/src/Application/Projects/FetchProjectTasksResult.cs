namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

public sealed class FetchProjectTasksResult(
    IEnumerable<FetchProjectTasksResult.TaskData> tasks)
{
    public ReadOnlyCollection<TaskData> Tasks { get; } = new([.. tasks]);

    public sealed class TaskData(
        Guid id,
        string title,
        int status)
    {
        public Guid Id { get; } = id;

        public string Title { get; } = title;

        public int Status { get; } = status;
    }
}

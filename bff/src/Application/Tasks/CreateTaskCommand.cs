namespace ProjectFollowUp.BFF.Application.Tasks;

using ProjectFollowUp.BFF.Application.Cqrs;
using ProjectFollowUp.BFF.Domain.Project;

public sealed class CreateTaskCommand(
    ProjectId projectId,
    Guid taskId,
    string title,
    string description,
    DateOnly? dueDate) : ICommand
{
    public ProjectId ProjectId { get; } = projectId;

    public Guid TaskId { get; } = taskId;

    public string Title { get; } = title;

    public string Description { get; } = description;

    public DateOnly? DueDate { get; } = dueDate;
}

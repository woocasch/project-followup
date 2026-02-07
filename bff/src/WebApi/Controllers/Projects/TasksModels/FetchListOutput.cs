namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.TasksModels;

using ProjectFollowUp.BFF.Domain.Project;

public readonly record struct FetchListOutput(
    FetchListOutput.TaskListItem[] Tasks)
{
    public readonly record struct TaskListItem(
        Guid Id,
        string Title,
        ProjectTaskStatus Status);
}

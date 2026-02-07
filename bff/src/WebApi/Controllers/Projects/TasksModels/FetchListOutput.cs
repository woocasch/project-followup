namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.TasksModels;

public readonly record struct FetchListOutput(
    FetchListOutput.TaskListItem[] Tasks)
{
    public readonly record struct TaskListItem(
        Guid Id,
        string Title,
        string Status);
}

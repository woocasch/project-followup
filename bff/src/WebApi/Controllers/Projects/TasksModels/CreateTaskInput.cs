namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.TasksModels;

public readonly record struct CreateTaskInput(
    string Title,
    string Description,
    DateTimeOffset? DueDate);

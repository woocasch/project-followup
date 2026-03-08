namespace ProjectFollowUp.BFF.Domain.UnitTests.ProjectTests;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;
using ProjectFollowUp.BFF.Domain.User;

public sealed class ProjectAggregateRootTests
{
    private ProjectAggregateRoot instance = default!;

    [Fact]
    public void WhenProjectIsCreatedThenProjectCreatedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var title = "Test Project Title";
        var description = "Test Project Description";
        var createdBy = UserId.NewId();
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCreated(projectId, title, description, createdBy, createdAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(ProjectCreated)
                    && ((ProjectCreated)e).ProjectId == projectId
                    && ((ProjectCreated)e).Title == title
                    && ((ProjectCreated)e).Description == description
                    && ((ProjectCreated)e).CreatedAt == createdAt,
                "ProjectCreated event was not raised with correct values."))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectIsChangedThenProjectDetailsChangedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var title = "Test Project Title";
        var description = "Test Project Description";
        var changedTitle = "Updated Project Title";
        var changedDescription = "Updated Project Description";
        var createdBy = UserId.NewId();
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        var changedAt = new DateTimeOffset(2024, 1, 3, 3, 4, 5, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCreated(projectId, title, description, createdBy, createdAt))
            .When(t => t.ProjectIsChanged(changedTitle, changedDescription, changedAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(ProjectDetailsChanged)
                    && ((ProjectDetailsChanged)e).ProjectId == projectId
                    && ((ProjectDetailsChanged)e).Title == changedTitle
                    && ((ProjectDetailsChanged)e).Description == changedDescription
                    && ((ProjectDetailsChanged)e).ChangedAt == changedAt,
                "ProjectDetailsChanged event was not raised with correct values."))
            .BDDfy();
    }

    [Fact]
    public void WhenTaskIsAddedThenTaskAddedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var taskId = Guid.NewGuid();
        var title = "Test Task Title";
        var description = "Test Task Description";
        var dueDate = new DateOnly(2024, 12, 31);
        var createdBy = UserId.NewId();
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        var taskCreatedAt = new DateTimeOffset(2024, 1, 3, 4, 5, 6, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCreated(projectId, "Project Title", "Project Description", createdBy, createdAt))
            .When(t => t.TaskIsAdded(taskId, title, description, dueDate, taskCreatedAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(TaskAdded)
                    && ((TaskAdded)e).ProjectId == projectId
                    && ((TaskAdded)e).TaskId == taskId
                    && ((TaskAdded)e).Title == title
                    && ((TaskAdded)e).Description == description
                    && ((TaskAdded)e).DueDate == dueDate
                    && ((TaskAdded)e).CreatedAt == taskCreatedAt,
                "TaskAdded event was not raised with correct values."))
            .BDDfy();
    }

    [Fact]
    public void WhenWorkIsStartedOnTaskThenTaskWorkStartedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var taskId = Guid.NewGuid();
        var createdBy = UserId.NewId();
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        var taskCreatedAt = new DateTimeOffset(2024, 1, 3, 4, 5, 6, TimeSpan.Zero);
        var startedAt = new DateTimeOffset(2024, 1, 4, 5, 6, 7, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCreated(projectId, "Project Title", "Project Description", createdBy, createdAt))
            .And(t => t.TaskIsAdded(taskId, "Task Title", "Task Description", null, taskCreatedAt))
            .When(t => t.WorkIsStartedOnTask(taskId, startedAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(TaskWorkStarted)
                    && ((TaskWorkStarted)e).ProjectId == projectId
                    && ((TaskWorkStarted)e).TaskId == taskId
                    && ((TaskWorkStarted)e).StartedAt == startedAt,
                "TaskWorkStarted event was not raised with correct values."))
            .BDDfy();
    }

    [Fact]
    public void WhenTaskIsCompletedThenTaskCompletedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var taskId = Guid.NewGuid();
        var createdBy = UserId.NewId();
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        var taskCreatedAt = new DateTimeOffset(2024, 1, 3, 4, 5, 6, TimeSpan.Zero);
        var completedAt = new DateTimeOffset(2024, 1, 5, 6, 7, 8, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCreated(projectId, "Project Title", "Project Description", createdBy, createdAt))
            .And(t => t.TaskIsAdded(taskId, "Task Title", "Task Description", null, taskCreatedAt))
            .When(t => t.TaskIsCompleted(taskId, completedAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(TaskCompleted)
                    && ((TaskCompleted)e).ProjectId == projectId
                    && ((TaskCompleted)e).TaskId == taskId
                    && ((TaskCompleted)e).CompletedAt == completedAt,
                "TaskCompleted event was not raised with correct values."))
            .BDDfy();
    }

    [Fact]
    public void WhenTaskIsRemovedThenTaskRemovedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var taskId = Guid.NewGuid();
        var createdBy = UserId.NewId();
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        var taskCreatedAt = new DateTimeOffset(2024, 1, 3, 4, 5, 6, TimeSpan.Zero);
        var removedAt = new DateTimeOffset(2024, 1, 6, 7, 8, 9, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCreated(projectId, "Project Title", "Project Description", createdBy, createdAt))
            .And(t => t.TaskIsAdded(taskId, "Task Title", "Task Description", null, taskCreatedAt))
            .When(t => t.TaskIsRemoved(taskId, removedAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(TaskRemoved)
                    && ((TaskRemoved)e).ProjectId == projectId
                    && ((TaskRemoved)e).TaskId == taskId
                    && ((TaskRemoved)e).RemovedAt == removedAt,
                "TaskRemoved event was not raised with correct values."))
            .BDDfy();
    }

    private void TaskIsAdded(
        Guid taskId,
        string title,
        string description,
        DateOnly? dueDate,
        DateTimeOffset createdAt)
    {
        this.instance.AddTask(taskId, title, description, dueDate, createdAt);
    }

    private void ProjectIsCreated(
        ProjectId projectId,
        string title,
        string description,
        UserId createdBy,
        DateTimeOffset createdAt)
    {
        this.instance = ProjectAggregateRoot.Create(
            projectId,
            title,
            description,
            createdBy,
            createdAt);
    }

    private void ProjectIsChanged(
        string newTitle,
        string newDescription,
        DateTimeOffset changedAt)
    {
        this.instance.ChangeDetails(newTitle, newDescription, changedAt);
    }

    private void WorkIsStartedOnTask(Guid taskId, DateTimeOffset startedAt)
    {
        this.instance.StartWorkOnTask(taskId, startedAt);
    }

    private void TaskIsCompleted(Guid taskId, DateTimeOffset completedAt)
    {
        this.instance.CompleteTask(taskId, completedAt);
    }

    private void TaskIsRemoved(Guid taskId, DateTimeOffset removedAt)
    {
        this.instance.RemoveTask(taskId, removedAt);
    }

    private void InstanceContainsEvent(Predicate<object> predicate, string message)
    {
        this.instance.GetUncommittedEvents()
            .ShouldContain(e => predicate(e), message);
    }
}

namespace ProjectFollowUp.BFF.Domain.Project;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectAggregateRoot : AggregateRootBase<ProjectId>
{
    private readonly Collection<TaskData> tasks = [];

    private ProjectAggregateRoot()
    {
    }

    public override Guid AggregateId => this.Id.Value;

    public string Title { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public DateTimeOffset CreatedAt { get; private set; }

    public IReadOnlyCollection<TaskData> Tasks => this.tasks;

    public static ProjectAggregateRoot Create(ProjectId projectId, string title, string description, DateTimeOffset createdAt)
    {
        var project = new ProjectAggregateRoot();
        var domainEvent = new ProjectCreated(projectId, title, description, createdAt);
        project.Apply(domainEvent);
        return project;
    }

    public static ProjectAggregateRoot Rehydrate(IEnumerable<IAggregateEvent> domainEvents)
    {
        var project = new ProjectAggregateRoot();
        project.RecreateFromHistory(domainEvents);
        return project;
    }

    public void ChangeDetails(string title, string description, DateTimeOffset changedAt)
    {
        var domainEvent = new ProjectDetailsChanged(this.Id, title, description, changedAt);
        this.Apply(domainEvent);
    }

    public void AddTask(Guid taskId, string title, string description, DateTimeOffset createdAt)
    {
        var domainEvent = new TaskAdded(this.Id, taskId, title, description, createdAt);
        this.Apply(domainEvent);
    }

    public void StartWorkOnTask(Guid taskId, DateTimeOffset startedAt)
    {
        var domainEvent = new TaskWorkStarted(this.Id, taskId, startedAt);
        this.Apply(domainEvent);
    }

    public void CompleteTask(Guid taskId, DateTimeOffset completedAt)
    {
        var domainEvent = new TaskCompleted(this.Id, taskId, completedAt);
        this.Apply(domainEvent);
    }

    public void RemoveTask(Guid taskId, DateTimeOffset removedAt)
    {
        var domainEvent = new TaskRemoved(this.Id, taskId, removedAt);
        this.Apply(domainEvent);
    }

    protected override void When(IAggregateEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ProjectCreated projectCreated:
                this.When(projectCreated);
                break;
            case ProjectDetailsChanged projectDetailsChanged:
                this.When(projectDetailsChanged);
                break;
            case TaskAdded taskAdded:
                this.When(taskAdded);
                break;
            case TaskWorkStarted taskWorkStarted:
                this.When(taskWorkStarted);
                break;
            case TaskCompleted taskCompleted:
                this.When(taskCompleted);
                break;
            case TaskRemoved taskRemoved:
                this.When(taskRemoved);
                break;
            default:
                throw new InvalidOperationException($"Unknown domain event type: {domainEvent.GetType().FullName}");
        }
    }

    private void When(ProjectCreated projectCreated)
    {
        this.Id = projectCreated.ProjectId;
        this.Title = projectCreated.Title;
        this.Description = projectCreated.Description;
        this.CreatedAt = projectCreated.CreatedAt;
    }

    private void When(ProjectDetailsChanged projectDetailsChanged)
    {
        this.Title = projectDetailsChanged.Title;
        this.Description = projectDetailsChanged.Description;
    }

    private void When(TaskAdded taskAdded)
    {
        var taskData = new TaskData(
            taskAdded.TaskId,
            taskAdded.Title,
            taskAdded.Description,
            TaskStatus.Created,
            taskAdded.CreatedAt);
        this.tasks.Add(taskData);
    }

    private void When(TaskWorkStarted taskWorkStarted)
    {
        this.ChangeTaskStatus(taskWorkStarted.TaskId, TaskStatus.InProgress);
    }

    private void When(TaskRemoved taskRemoved)
    {
        var task = this.tasks.Single(t => t.TaskId == taskRemoved.TaskId);
        this.tasks.Remove(task);
    }

    private void When(TaskCompleted taskCompleted)
    {
        this.ChangeTaskStatus(taskCompleted.TaskId, TaskStatus.Completed);
    }

    private void ChangeTaskStatus(Guid taskId, TaskStatus status)
    {
        var task = this.tasks.Single(t => t.TaskId == taskId);
        var taskIndex = this.tasks.IndexOf(task);
        task = task with
        {
            Status = status,
        };
        this.tasks[taskIndex] = task;
    }
}

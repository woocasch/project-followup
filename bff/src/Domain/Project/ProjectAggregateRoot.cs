namespace ProjectFollowUp.BFF.Domain.Project;

using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectAggregateRoot : AggregateRootBase<ProjectId>
{
    private ProjectAggregateRoot()
    {
    }

    public override Guid AggregateId => this.Id.Value;

    public string Title { get; private set; } = null!;

    public string Description { get; private set; } = null!;

    public DateTimeOffset CreatedAt { get; private set; }

    public static ProjectAggregateRoot Create(ProjectId projectId, string title, string description, DateTimeOffset createdAt)
    {
        var project = new ProjectAggregateRoot();
        var domainEvent = new ProjectCreated(projectId, title, description, createdAt);
        project.Apply(domainEvent);
        return project;
    }

    public static ProjectAggregateRoot Rehydrate(IEnumerable<IEvent> domainEvents)
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

    protected override void When(IEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ProjectCreated projectCreated:
                this.When(projectCreated);
                break;
            case ProjectDetailsChanged projectDetailsChanged:
                this.When(projectDetailsChanged);
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
}

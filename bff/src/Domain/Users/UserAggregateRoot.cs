namespace ProjectFollowUp.BFF.Domain.Users;

using ProjectFollowUp.BFF.Domain.Projects;
using ProjectFollowUp.BFF.Domain.Projects.ProjectEvents;
using ProjectFollowUp.BFF.Domain.Users.UserEvents;

public sealed class UserAggregateRoot : AggregateRootBase<UserId>
{
    private UserAggregateRoot()
    {
    }

    public string DisplayName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public DateTimeOffset CreatedAt { get; private set; }

    public static UserAggregateRoot Create(
        UserId projectId,
        string displayName,
        string email,
        DateTimeOffset createdAt)
    {
        var project = new UserAggregateRoot();
        var domainEvent = new UserCreated(
            projectId,
            displayName,
            email,
            createdAt);
        project.Apply(domainEvent);
        return project;
    }


    public static UserAggregateRoot Rehydrate(IEnumerable<object> domainEvents)
    {
        var project = new UserAggregateRoot();
        project.RecreateFromHistory(domainEvents);
        return project;
    }

    protected override void When(object domainEvent)
    {
        switch (domainEvent)
        {
            case UserCreated userCreated:
                this.When(userCreated);
                break;
            default:
                throw new InvalidOperationException($"Unknown domain event type: {domainEvent.GetType().FullName}");
        }
    }

    private void When(UserCreated userCreated)
    {
        this.Id = userCreated.Id;
        this.DisplayName = userCreated.DisplayName;
        this.Email = userCreated.Email;
        this.CreatedAt = userCreated.CreatedAt;
    }
}

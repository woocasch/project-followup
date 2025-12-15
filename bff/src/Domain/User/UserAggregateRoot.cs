namespace ProjectFollowUp.BFF.Domain.User;

using ProjectFollowUp.BFF.Domain.User.Events;

public sealed class UserAggregateRoot : AggregateRootBase<UserId>
{
    private UserAggregateRoot()
    {
    }

    public string DisplayName { get; private set; } = null!;

    public EmailAddress Email { get; private set; } = null!;

    public DateTimeOffset CreatedAt { get; private set; }

    public static UserAggregateRoot Create(
        UserId userId,
        string displayName,
        EmailAddress email,
        DateTimeOffset createdAt)
    {
        var user = new UserAggregateRoot();
        var domainEvent = new UserCreated(
            userId,
            displayName,
            email,
            createdAt);
        user.Apply(domainEvent);
        return user;
    }


    public static UserAggregateRoot Rehydrate(IEnumerable<object> domainEvents)
    {
        var user = new UserAggregateRoot();
        user.RecreateFromHistory(domainEvents);
        return user;
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

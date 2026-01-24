namespace ProjectFollowUp.BFF.Domain.ActivationLink;

using System;

using ProjectFollowUp.BFF.Domain.ActivationLink.Events;
using ProjectFollowUp.BFF.Domain.User;

public sealed class ActivationLinkAggregateRoot : AggregateRootBase<ActivationLinkId>
{
    private ActivationLinkAggregateRoot()
    {
    }

    public override Guid AggregateId => this.Id.Value;

    public UserId UserId { get; private set; } = default!;

    public string LinkCode { get; private set; } = string.Empty;

    public bool IsUsed { get; private set; }

    public static ActivationLinkAggregateRoot Create(
        UserId userId,
        string linkCode)
    {
        var id = ActivationLinkId.NewId();
        var @event = new LinkCreated(
            id,
            userId,
            linkCode);
        var activationLink = new ActivationLinkAggregateRoot();
        activationLink.Apply(@event);
        return activationLink;
    }

    public static ActivationLinkAggregateRoot Rehydrate(IEnumerable<object> domainEvents)
    {
        var user = new ActivationLinkAggregateRoot();
        user.RecreateFromHistory(domainEvents);
        return user;
    }

    protected override void When(object domainEvent)
    {
        switch (domainEvent)
        {
            case LinkCreated linkCreated:
                this.When(linkCreated);
                break;
            default:
                throw new InvalidOperationException($"Unknown domain event type: {domainEvent.GetType().FullName}");
        }
    }

    private void When(LinkCreated linkCreated)
    {
        this.Id = linkCreated.LinkId;
        this.UserId = linkCreated.UserId;
        this.LinkCode = linkCreated.LinkCode;
        this.IsUsed = false;
    }
}

namespace ProjectFollowUp.BFF.Domain.UnitTests.ActivationLinkTests;

using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.ActivationLink.Events;
using ProjectFollowUp.BFF.Domain.User;

public sealed class ActivationLinkAggregateRootTests
{
    private ActivationLinkAggregateRoot instance = default!;

    [Fact]
    public void WhenActivationLinkIsCreatedThenActivationLinkCreatedEventIsRaised()
    {
        var userIdValue = Guid.NewGuid();
        var userId = UserId.FromGuid(userIdValue);
        var linkCode = "test-link-code";
        this.Given(t => t.InstanceIsCreated(userId, linkCode))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(LinkCreated)
                    && ((LinkCreated)e).LinkId.Value != Guid.Empty
                    && ((LinkCreated)e).UserId == userId
                    && ((LinkCreated)e).LinkCode == linkCode,
                "ActivationLinkCreated event was not raised with correct values."))
            .BDDfy();
    }

    private void InstanceIsCreated(UserId userId, string linkCode)
    {
        this.instance = ActivationLinkAggregateRoot.Create(
            userId,
            linkCode);
    }

    private void InstanceContainsEvent(Predicate<object> predicate, string message)
    {
        this.instance.GetUncommitedDomainEvents()
            .ShouldContain(e => predicate(e), message);
    }
}

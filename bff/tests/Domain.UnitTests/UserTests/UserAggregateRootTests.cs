namespace ProjectFollowUp.BFF.Domain.UnitTests.UserTests;

using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Domain.User.Events;

public sealed class UserAggregateRootTests
{
    private UserAggregateRoot instance = default!;

    [Fact]
    public void WhenUserIsCreatedThenUserCreatedEventIsRaised()
    {
        var userIdValue = Guid.NewGuid();
        var userId = UserId.FromGuid(userIdValue);
        var credentialsId = Guid.NewGuid();
        var displayName = "DISPLAY_NAME";
        var email = EmailAddress.FromString("user@domain.com");
        var createdAt = new DateTimeOffset(2026, 1, 2, 3, 4, 5, TimeSpan.Zero);
        this.Given(t => t.InstanceIsCreated(userId, credentialsId, displayName, email, createdAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(UserCreated)
                    && ((UserCreated)e).UserId == userId
                    && ((UserCreated)e).CredentialsId == credentialsId
                    && ((UserCreated)e).DisplayName == displayName
                    && ((UserCreated)e).Email == email
                    && ((UserCreated)e).CreatedAt == createdAt,
                "UserCreated event was not raised with correct values."))
            .BDDfy();
    }

    private void InstanceIsCreated(
        UserId userId,
        Guid credentialsId,
        string displayName,
        EmailAddress emailAddress,
        DateTimeOffset createdAt)
    {
        this.instance = UserAggregateRoot.Create(
            userId,
            credentialsId,
            displayName,
            emailAddress,
            createdAt);
    }

    private void InstanceContainsEvent(Predicate<object> predicate, string message)
    {
        this.instance.GetUncommittedEvents()
            .ShouldContain(e => predicate(e), message);
    }
}

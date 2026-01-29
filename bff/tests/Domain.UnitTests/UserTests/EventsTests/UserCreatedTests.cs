namespace ProjectFollowUp.BFF.Domain.UnitTests.UserTests.EventsTests;

using ProjectFollowUp.BFF.Domain.User;
using ProjectFollowUp.BFF.Domain.User.Events;

public sealed class UserCreatedTests
{
    private UserCreated instance;

    private string serializedInstance = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedInstanceIsAsExpected()
    {
        var userIdValue = Guid.Parse("6ce35041-f3b0-4100-b2fa-165113e51264");
        var credentialsId = Guid.Parse("d50c4cec-5fde-4351-b8e3-f8daa1775ea9");
        var testInstance = new UserCreated(
            UserId.FromGuid(userIdValue),
            credentialsId,
            "DISPLAY_NAME",
            EmailAddress.FromString("user@domain.com"),
            new DateTimeOffset(2025, 4, 5, 6, 7, 8, TimeSpan.Zero));
        var expectedSerializedValue = """{"userId":"6ce35041-f3b0-4100-b2fa-165113e51264","credentialsId":"d50c4cec-5fde-4351-b8e3-f8daa1775ea9","displayName":"DISPLAY_NAME","email":"user@domain.com","createdAt":"2025-04-05T06:07:08+00:00"}""";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceIsDeserializedThenInstanceIsAsExpected()
    {
        var serializedValue = """{"userId":"2adf7efd-0dc5-43ba-812a-4b5fc1388f64","credentialsId":"84493ee7-6265-4715-8534-a2290e93c692","displayName":"DISPLAY_NAME","email":"user@domain.com","createdAt":"2025-04-05T06:07:08+00:00"}""";
        var expectedInstance = new UserCreated(
            UserId.FromGuid(Guid.Parse("2adf7efd-0dc5-43ba-812a-4b5fc1388f64")),
            Guid.Parse("84493ee7-6265-4715-8534-a2290e93c692"),
            "DISPLAY_NAME",
            EmailAddress.FromString("user@domain.com"),
            new DateTimeOffset(2025, 4, 5, 6, 7, 8, TimeSpan.Zero));
        this.Given(t => t.SerializedInstanceIsSetTo(serializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void InstanceIsSetTo(UserCreated value) => this.instance = value;

    private void SerializedInstanceIsSetTo(string value) => this.serializedInstance = value;

    private void InstanceIsSerialized()
    {
        this.serializedInstance = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedInstance.Deserialize<UserCreated>();
    }

    private void InstanceIs(UserCreated value)
    {
        this.instance.ShouldBe(value);
    }

    private void SerializedInstanceIs(string value)
    {
        this.serializedInstance.ShouldBe(value);
    }
}

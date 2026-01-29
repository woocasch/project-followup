namespace ProjectFollowUp.BFF.Domain.UnitTests.UserTests;

using ProjectFollowUp.BFF.Domain.User;

public sealed class UserIdTests
{
    private UserId instance;

    private string serializedValue = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedValueIsCorrect()
    {
        var idValue = Guid.Parse("27bf351a-1f40-4ddf-836d-cfc5f30b7627");
        var testInstance = UserId.FromGuid(idValue);
        var expectedSerializedValue = $"\"{idValue}\"";
        this.Given(t => t.UserIdIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedValueIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceIsDeserializedThenInstanceIsCorrect()
    {
        var idValue = Guid.Parse("431ba636-d1e6-4c26-87b7-b8c02e608c89");
        var expectedInstance = UserId.FromGuid(idValue);
        var actualSerializedValue = $"\"{idValue}\"";
        this.Given(t => t.SerializedValueIsSetTo(actualSerializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void UserIdIsSetTo(UserId value) => this.instance = value;

    private void SerializedValueIsSetTo(string value) => this.serializedValue = value;

    private void InstanceIsSerialized()
    {
        this.serializedValue = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedValue.Deserialize<UserId>()!;
    }

    private void InstanceIs(UserId expected)
    {
        this.instance.ShouldBe(expected);
    }

    private void SerializedValueIs(string expected)
    {
        this.serializedValue.ShouldBe(expected);
    }
}

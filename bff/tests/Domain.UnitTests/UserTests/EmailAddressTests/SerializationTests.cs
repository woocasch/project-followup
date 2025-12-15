namespace ProjectFollowUp.BFF.Domain.UnitTests.UserTests.EmailAddressTests;

using System.Text.Json;

using ProjectFollowUp.BFF.Domain.User;

public sealed class SerializationTests
{
    private EmailAddress instance = null!;

    private string serializedValue = null!;

    private Action serializationAction = null!;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedValueIsCorrect()
    {
        const string emailAddress = "user@domain.com";
        const string expectedSerializedValue = """{"value":"user@domain.com"}""";
        this.Given(t => t.InstanceIsCreated(emailAddress))
            .And(t => t.InstanceIsSerialized())
            .When(t => t.ExceptionIsNotThrown())
            .Then(t => t.SerializedValueIs(v => v == expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenSerializedValueIsDeserializedThenInstanceIsCorrect()
    {
        const string actualSerializedValue = """{"value":"user@domain.com"}""";
        const string expectedEmailAddress = "user@domain.com";
        this.Given(t => t.SerializedValueIsSetTo(actualSerializedValue))
            .And(t => t.InstanceIsDeserialized())
            .When(t => t.ExceptionIsNotThrown())
            .Then(t => t.InstanceValueIs(v => v.Value == expectedEmailAddress))
            .BDDfy();
    }

    private void InstanceIsCreated(string value)
    {
        this.instance = EmailAddress.FromString(value);
    }

    private void SerializedValueIsSetTo(string value)
    {
        this.serializedValue = value;
    }

    private void InstanceIsSerialized()
    {
        this.serializationAction = () => this.serializedValue = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.serializationAction = () => this.instance = this.serializedValue.Deserialize<EmailAddress>()!;
    }

    private void ExceptionIsNotThrown()
    {
        this.serializationAction.ShouldNotThrow();
    }

    private void SerializedValueIs(Predicate<string> predicate)
    {
        predicate(this.serializedValue).ShouldBeTrue();
    }

    private void InstanceValueIs(Predicate<EmailAddress> predicate)
    {
        predicate(this.instance).ShouldBeTrue();
    }
}

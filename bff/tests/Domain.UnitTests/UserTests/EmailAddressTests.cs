namespace ProjectFollowUp.BFF.Domain.UnitTests.UserTests;

using System;

using ProjectFollowUp.BFF.Domain.User;

public class EmailAddressTests
{
    private string emailAddress = null!;

    private EmailAddress instance = null!;

    private string serializedValue = null!;

    private Action action = null!;

    [Fact]
    public void WhenValidEmailIsCreatedThenNoExceptionIsThrownAndInstanceIsCreated()
    {
        const string expectedValue = "test@domain.com";
        this.Given(t => t.EmailAddressIs(expectedValue))
            .And(t => t.InstanceIsCreatedFromString())
            .When(t => t.ActionDoesNotThrow())
            .Then(t => t.InstanceValueIs(expectedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenEmptyEmailIsCreatedThenArgumentExceptionIsThrown()
    {
        this.Given(t => t.EmailAddressIs(string.Empty))
            .And(t => t.InstanceIsCreatedFromString())
            .When(t => t.ActionThrowsArgumentException())
            .BDDfy();
    }

    [Fact]
    public void WhenInvalidEmailIsCreatedThenArgumentExceptionIsThrown()
    {
        const string invalidEmail = "invalid-email";
        this.Given(t => t.EmailAddressIs(invalidEmail))
            .And(t => t.InstanceIsCreatedFromString())
            .When(t => t.ActionThrowsArgumentException())
            .BDDfy();
    }
    [Fact]
    public void WhenInstanceIsSerializedThenSerializedValueIsCorrect()
    {
        const string emailAddress = "user@domain.com";
        const string expectedSerializedValue = "\"user@domain.com\"";
        this.Given(t => t.InstanceIsCreated(emailAddress))
            .And(t => t.InstanceIsSerialized())
            .When(t => t.ActionDoesNotThrow())
            .Then(t => t.SerializedValueIs(v => v == expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenSerializedValueIsDeserializedThenInstanceIsCorrect()
    {
        const string actualSerializedValue = "\"user@domain.com\"";
        const string expectedEmailAddress = "user@domain.com";
        this.Given(t => t.SerializedValueIsSetTo(actualSerializedValue))
            .And(t => t.InstanceIsDeserialized())
            .When(t => t.ActionDoesNotThrow())
            .Then(t => t.InstanceValueIs(v => v.Value == expectedEmailAddress))
            .BDDfy();
    }


    private void EmailAddressIs(string value)
    {
        this.emailAddress = value;
    }

    private void InstanceIsCreatedFromString()
    {
        this.action = () => this.instance = EmailAddress.FromString(this.emailAddress);
    }

    private void ActionDoesNotThrow()
    {
        this.action.ShouldNotThrow();
    }

    private void ActionThrowsArgumentException()
    {
        var exception = this.action.ShouldThrow<ArgumentException>();
        exception.ParamName.ShouldBe("email");
    }

    private void InstanceValueIs(string expectedValue)
    {
        this.instance.Value.ShouldBe(expectedValue);
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
        this.action = () => this.serializedValue = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.action = () => this.instance = this.serializedValue.Deserialize<EmailAddress>()!;
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

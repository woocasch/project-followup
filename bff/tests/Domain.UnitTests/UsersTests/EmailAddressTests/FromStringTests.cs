namespace ProjectFollowUp.BFF.Domain.UnitTests.UserTests.EmailAddressTests;

using ProjectFollowUp.BFF.Domain.User;

public sealed class FromStringTests
{
    private string emailAddress = null!;

    private EmailAddress instance = null!;

    private Action factoryAction = null!;

    [Fact]
    public void WhenValidEmailIsCreatedThenNoExceptionIsThrownAndInstanceIsCreated()
    {
        const string expectedValue = "test@domain.com";
        this.Given(t => t.EmailAddressIs(expectedValue))
            .And(t => t.InstanceIsCreated())
            .When(t => t.ExceptionIsNotThrown())
            .Then(t => t.InstanceValueIs(expectedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenEmptyEmailIsCreatedThenArgumentExceptionIsThrown()
    {
        this.Given(t => t.EmailAddressIs(string.Empty))
            .And(t => t.InstanceIsCreated())
            .When(t => t.ArgumentExceptionIsThrown())
            .BDDfy();
    }

    [Fact]
    public void WhenInvalidEmailIsCreatedThenArgumentExceptionIsThrown()
    {
        const string invalidEmail = "invalid-email";
        this.Given(t => t.EmailAddressIs(invalidEmail))
            .And(t => t.InstanceIsCreated())
            .When(t => t.ArgumentExceptionIsThrown())
            .BDDfy();
    }

    private void EmailAddressIs(string value)
    {
        this.emailAddress = value;
    }

    private void InstanceIsCreated()
    {
        this.factoryAction = () => this.instance = EmailAddress.FromString(this.emailAddress);
    }

    private void ExceptionIsNotThrown()
    {
        this.factoryAction.ShouldNotThrow();
    }

    private void ArgumentExceptionIsThrown()
    {
        var exception = this.factoryAction.ShouldThrow<ArgumentException>();
        exception.ParamName.ShouldBe("email");
    }

    private void InstanceValueIs(string expectedValue)
    {
        this.instance.Value.ShouldBe(expectedValue);
    }
}

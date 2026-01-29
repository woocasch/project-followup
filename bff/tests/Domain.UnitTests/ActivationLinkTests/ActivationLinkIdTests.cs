namespace ProjectFollowUp.BFF.Domain.UnitTests.ActivationLinkTests;

using ProjectFollowUp.BFF.Domain.ActivationLink;

public sealed class ActivationLinkIdTests
{
    private ActivationLinkId instance;

    private string serializedValue = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedValueIsCorrect()
    {
        var idValue = Guid.Parse("BE583F0D-B58A-4C99-B02B-F0DF7CAE4F0C");
        var testInstance = ActivationLinkId.FromGuid(idValue);
        var expectedSerializedValue = $"\"{idValue}\"";
        this.Given(t => t.ActivationLinkIdIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedValueIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceIsDeserializedThenInstanceIsCorrect()
    {
        var idValue = Guid.Parse("0B2DB58D-E1D6-4679-9687-BB2A677A5B44");
        var expectedInstance = ActivationLinkId.FromGuid(idValue);
        var actualSerializedValue = $"\"{idValue}\"";
        this.Given(t => t.SerializedValueIsSetTo(actualSerializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void ActivationLinkIdIsSetTo(ActivationLinkId value) => this.instance = value;

    private void SerializedValueIsSetTo(string value) => this.serializedValue = value;

    private void InstanceIsSerialized()
    {
        this.serializedValue = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedValue.Deserialize<ActivationLinkId>()!;
    }

    private void InstanceIs(ActivationLinkId expected)
    {
        this.instance.ShouldBe(expected);
    }

    private void SerializedValueIs(string expected)
    {
        this.serializedValue.ShouldBe(expected);
    }
}

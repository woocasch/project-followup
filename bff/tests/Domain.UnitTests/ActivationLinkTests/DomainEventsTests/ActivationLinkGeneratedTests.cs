namespace ProjectFollowUp.BFF.Domain.UnitTests.ActivationLinkTests.DomainEventsTests;

using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.ActivationLink.DomainEvents;

public sealed class ActivationLinkGeneratedTests
{
    private ActivationLinkGenerated instance;

    private string serializedInstance = string.Empty;

    [Fact]
    public void WhenActivationLinkGeneratedIsSerializedThenSerializationIsCorrect()
    {
        var activationLinkIdText = "f32bc48b-fb37-4da3-b59a-daa303a0c1ec";
        var activationLinkIdValue = Guid.Parse(activationLinkIdText);
        var activationLinkId = ActivationLinkId.FromGuid(activationLinkIdValue);
        var testInstance = new ActivationLinkGenerated(
            activationLinkId);
        var expectedSerializedInstance = "{\"activationLinkId\":\"f32bc48b-fb37-4da3-b59a-daa303a0c1ec\"}";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedInstance))
            .BDDfy();
    }

    [Fact]
    public void WhenActivationLinkGeneratedIsDeserializedThenInstanceIsCorrect()
    {
        var activationLinkIdText = "a1d5c3e4-5b6f-4782-8c3d-9e1f2a3b4c5d";
        var activationLinkIdValue = Guid.Parse(activationLinkIdText);
        var expectedActivationLinkId = ActivationLinkId.FromGuid(activationLinkIdValue);
        var expectedInstance = new ActivationLinkGenerated(
            expectedActivationLinkId);
        var actualSerializedInstance = "{\"activationLinkId\":\"a1d5c3e4-5b6f-4782-8c3d-9e1f2a3b4c5d\"}";
        this.Given(t => t.SerializedInstanceIsSetTo(actualSerializedInstance))
            .When(t => t.SerializedInstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void InstanceIsSetTo(ActivationLinkGenerated value) => this.instance = value;

    private void SerializedInstanceIsSetTo(string value) => this.serializedInstance = value;

    private void InstanceIsSerialized()
    {
        this.serializedInstance = SerializationHelper.Serialize(this.instance);
    }

    private void SerializedInstanceIsDeserialized()
    {
        this.instance = SerializationHelper.Deserialize<ActivationLinkGenerated>(this.serializedInstance);
    }

    private void SerializedInstanceIs(string expected)
    {
        this.serializedInstance.ShouldBe(expected);
    }

    private void InstanceIs(ActivationLinkGenerated expected)
    {
        this.instance.ShouldBe(expected);
    }
}

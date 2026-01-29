namespace ProjectFollowUp.BFF.Domain.UnitTests.ActivationLinkTests.EventsTests;

using ProjectFollowUp.BFF.Domain.ActivationLink;
using ProjectFollowUp.BFF.Domain.ActivationLink.Events;
using ProjectFollowUp.BFF.Domain.User;

public sealed class LinkCreatedTests
{
    private LinkCreated instance;

    private string serializedInstance = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedInstanceIsAsExpected()
    {
        var linkIdValue = Guid.Parse("5afc39b3-6739-49a3-991d-46fea55a7ebe");
        var userIdValue = Guid.Parse("2309f97d-b8ce-448b-bedb-60ed0362cde4");
        var testInstance = new LinkCreated(
            ActivationLinkId.FromGuid(linkIdValue),
            UserId.FromGuid(userIdValue),
            "LINK_CODE");
        var expectedSerializedValue = """{"linkId":"5afc39b3-6739-49a3-991d-46fea55a7ebe","userId":"2309f97d-b8ce-448b-bedb-60ed0362cde4","linkCode":"LINK_CODE"}""";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedValue))
            .BDDfy();
    }

    private void InstanceIsSetTo(LinkCreated value) => this.instance = value;

    private void SerializedInstanceIsSetTo(string value) => this.serializedInstance = value;

    private void InstanceIsSerialized()
    {
        this.serializedInstance = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedInstance.Deserialize<LinkCreated>();
    }

    private void InstanceIs(LinkCreated value)
    {
        this.instance.ShouldBe(value);
    }

    private void SerializedInstanceIs(string value)
    {
        this.serializedInstance.ShouldBe(value);
    }
}

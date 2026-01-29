namespace ProjectFollowUp.BFF.Domain.UnitTests.ProjectTests.EventsTests;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectCreatedTests
{
    private ProjectCreated instance;

    private string serializedInstance = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedInstanceIsAsExpected()
    {
        var projectIdValue = Guid.Parse("e3e77038-be0a-4080-b851-daa7c52db072");
        var testInstance = new ProjectCreated(
            ProjectId.FromGuid(projectIdValue),
            "PROJECT_TITLE",
            "PROJECT_DESCRIPTION",
            new DateTimeOffset(2025, 4, 5, 6, 7, 8, TimeSpan.Zero));
        var expectedSerializedValue = """{"projectId":"e3e77038-be0a-4080-b851-daa7c52db072","title":"PROJECT_TITLE","description":"PROJECT_DESCRIPTION","createdAt":"2025-04-05T06:07:08+00:00"}""";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceIsDeserializedThenInstanceIsAsExpected()
    {
        var serializedValue = """{"projectId":"0a4afa70-8683-4276-8173-4f456c673e83","title":"DIFFERENT_PROJECT_TITLE","description":"DIFFERENT_PROJECT_DESCRIPTION","createdAt":"2026-07-08T09:10:11+00:00"}""";
        var expectedInstance = new ProjectCreated(
            ProjectId.FromGuid(Guid.Parse("0a4afa70-8683-4276-8173-4f456c673e83")),
            "DIFFERENT_PROJECT_TITLE",
            "DIFFERENT_PROJECT_DESCRIPTION",
            new DateTimeOffset(2026, 7, 8, 9, 10, 11, TimeSpan.Zero));
        this.Given(t => t.SerializedInstanceIsSetTo(serializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void InstanceIsSetTo(ProjectCreated value) => this.instance = value;

    private void SerializedInstanceIsSetTo(string value) => this.serializedInstance = value;

    private void InstanceIsSerialized()
    {
        this.serializedInstance = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedInstance.Deserialize<ProjectCreated>();
    }

    private void InstanceIs(ProjectCreated value)
    {
        this.instance.ShouldBe(value);
    }

    private void SerializedInstanceIs(string value)
    {
        this.serializedInstance.ShouldBe(value);
    }
}

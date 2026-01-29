namespace ProjectFollowUp.BFF.Domain.UnitTests.ProjectTests.EventsTests;

using System;
using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class ProjectDetailsChangedTests
{
    private ProjectDetailsChanged instance;

    private string serializedInstance = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedInstanceIsAsExpected()
    {
        var projectIdValue = Guid.Parse("009c17c5-a525-40c2-a70f-765f600a2e74");
        var testInstance = new ProjectDetailsChanged(
            ProjectId.FromGuid(projectIdValue),
            "PROJECT_TITLE",
            "PROJECT_DESCRIPTION",
            new DateTimeOffset(2025, 4, 5, 6, 7, 8, TimeSpan.Zero));
        var expectedSerializedValue = """{"projectId":"009c17c5-a525-40c2-a70f-765f600a2e74","title":"PROJECT_TITLE","description":"PROJECT_DESCRIPTION","changedAt":"2025-04-05T06:07:08+00:00"}""";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceIsDeserializedThenInstanceIsAsExpected()
    {
        var serializedValue = """{"projectId":"009c17c5-a525-40c2-a70f-765f600a2e74","title":"DIFFERENT_PROJECT_TITLE","description":"DIFFERENT_PROJECT_DESCRIPTION","changedAt":"2026-07-08T09:10:11+00:00"}""";
        var expectedInstance = new ProjectDetailsChanged(
            ProjectId.FromGuid(Guid.Parse("009c17c5-a525-40c2-a70f-765f600a2e74")),
            "DIFFERENT_PROJECT_TITLE",
            "DIFFERENT_PROJECT_DESCRIPTION",
            new DateTimeOffset(2026, 7, 8, 9, 10, 11, TimeSpan.Zero));
        this.Given(t => t.SerializedInstanceIsSetTo(serializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void InstanceIsSetTo(ProjectDetailsChanged value) => this.instance = value;

    private void SerializedInstanceIsSetTo(string value) => this.serializedInstance = value;

    private void InstanceIsSerialized()
    {
        this.serializedInstance = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedInstance.Deserialize<ProjectDetailsChanged>();
    }

    private void InstanceIs(ProjectDetailsChanged value)
    {
        this.instance.ShouldBe(value);
    }

    private void SerializedInstanceIs(string value)
    {
        this.serializedInstance.ShouldBe(value);
    }
}

namespace ProjectFollowUp.BFF.Domain.UnitTests.ProjectTests.EventsTests;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;

public sealed class TaskAddedTests
{
    private TaskAdded instance;

    private string serializedInstance = string.Empty;

    [Fact]
    public void WhenInstanceIsSerializedThenSerializedInstanceIsAsExpected()
    {
        var projectIdValue = Guid.Parse("e3e77038-be0a-4080-b851-daa7c52db072");
        var taskIdValue = Guid.Parse("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d");
        var testInstance = new TaskAdded(
            ProjectId.FromGuid(projectIdValue),
            taskIdValue,
            "TASK_TITLE",
            "TASK_DESCRIPTION",
            new DateOnly(2025, 12, 31),
            ProjectTaskStatus.Created,
            new DateTimeOffset(2025, 4, 5, 6, 7, 8, TimeSpan.Zero));
        var expectedSerializedValue = """{"projectId":"e3e77038-be0a-4080-b851-daa7c52db072","taskId":"a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d","title":"TASK_TITLE","description":"TASK_DESCRIPTION","dueDate":"2025-12-31","status":"Created","createdAt":"2025-04-05T06:07:08+00:00"}""";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceWithNullDueDateIsSerializedThenSerializedInstanceIsAsExpected()
    {
        var projectIdValue = Guid.Parse("e3e77038-be0a-4080-b851-daa7c52db072");
        var taskIdValue = Guid.Parse("a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d");
        var testInstance = new TaskAdded(
            ProjectId.FromGuid(projectIdValue),
            taskIdValue,
            "TASK_TITLE",
            "TASK_DESCRIPTION",
            null,
            ProjectTaskStatus.Created,
            new DateTimeOffset(2025, 4, 5, 6, 7, 8, TimeSpan.Zero));
        var expectedSerializedValue = """{"projectId":"e3e77038-be0a-4080-b851-daa7c52db072","taskId":"a1b2c3d4-e5f6-4a5b-8c9d-0e1f2a3b4c5d","title":"TASK_TITLE","description":"TASK_DESCRIPTION","dueDate":null,"status":"Created","createdAt":"2025-04-05T06:07:08+00:00"}""";
        this.Given(t => t.InstanceIsSetTo(testInstance))
            .When(t => t.InstanceIsSerialized())
            .Then(t => t.SerializedInstanceIs(expectedSerializedValue))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceIsDeserializedThenInstanceIsAsExpected()
    {
        var serializedValue = """{"projectId":"0a4afa70-8683-4276-8173-4f456c673e83","taskId":"f1e2d3c4-b5a6-4c5d-8e9f-0a1b2c3d4e5f","title":"DIFFERENT_TASK_TITLE","description":"DIFFERENT_TASK_DESCRIPTION","dueDate":"2026-11-30","status":"InProgress","createdAt":"2026-07-08T09:10:11+00:00"}""";
        var expectedInstance = new TaskAdded(
            ProjectId.FromGuid(Guid.Parse("0a4afa70-8683-4276-8173-4f456c673e83")),
            Guid.Parse("f1e2d3c4-b5a6-4c5d-8e9f-0a1b2c3d4e5f"),
            "DIFFERENT_TASK_TITLE",
            "DIFFERENT_TASK_DESCRIPTION",
            new DateOnly(2026, 11, 30),
            ProjectTaskStatus.InProgress,
            new DateTimeOffset(2026, 7, 8, 9, 10, 11, TimeSpan.Zero));
        this.Given(t => t.SerializedInstanceIsSetTo(serializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    [Fact]
    public void WhenInstanceWithNullDueDateIsDeserializedThenInstanceIsAsExpected()
    {
        var serializedValue = """{"projectId":"0a4afa70-8683-4276-8173-4f456c673e83","taskId":"f1e2d3c4-b5a6-4c5d-8e9f-0a1b2c3d4e5f","title":"DIFFERENT_TASK_TITLE","description":"DIFFERENT_TASK_DESCRIPTION","dueDate":null,"status":"Completed","createdAt":"2026-07-08T09:10:11+00:00"}""";
        var expectedInstance = new TaskAdded(
            ProjectId.FromGuid(Guid.Parse("0a4afa70-8683-4276-8173-4f456c673e83")),
            Guid.Parse("f1e2d3c4-b5a6-4c5d-8e9f-0a1b2c3d4e5f"),
            "DIFFERENT_TASK_TITLE",
            "DIFFERENT_TASK_DESCRIPTION",
            null,
            ProjectTaskStatus.Completed,
            new DateTimeOffset(2026, 7, 8, 9, 10, 11, TimeSpan.Zero));
        this.Given(t => t.SerializedInstanceIsSetTo(serializedValue))
            .When(t => t.InstanceIsDeserialized())
            .Then(t => t.InstanceIs(expectedInstance))
            .BDDfy();
    }

    private void InstanceIsSetTo(TaskAdded value) => this.instance = value;

    private void SerializedInstanceIsSetTo(string value) => this.serializedInstance = value;

    private void InstanceIsSerialized()
    {
        this.serializedInstance = this.instance.Serialize();
    }

    private void InstanceIsDeserialized()
    {
        this.instance = this.serializedInstance.Deserialize<TaskAdded>();
    }

    private void InstanceIs(TaskAdded value)
    {
        this.instance.ShouldBe(value);
    }

    private void SerializedInstanceIs(string value)
    {
        this.serializedInstance.ShouldBe(value);
    }
}

namespace ProjectFollowUp.BFF.Domain.UnitTests.ProjectTests;

using ProjectFollowUp.BFF.Domain.Project;
using ProjectFollowUp.BFF.Domain.Project.Events;
using ProjectFollowUp.BFF.Domain.User;

public sealed class ProjectAggregateRootTests
{
    private ProjectAggregateRoot instance = default!;

    [Fact]
    public void WhenProjectIsCreatedThenProjectCreatedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var title = "Test Project Title";
        var description = "Test Project Description";
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCrated(projectId, title, description, createdAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(ProjectCreated)
                    && ((ProjectCreated)e).ProjectId == projectId
                    && ((ProjectCreated)e).Title == title
                    && ((ProjectCreated)e).Description == description
                    && ((ProjectCreated)e).CreatedAt == createdAt,
                "ProjectCreated event was not raised with correct values."))
            .BDDfy();
    }

    [Fact]
    public void WhenProjectIsChangedThenProjectDetailsChangedEventIsRaised()
    {
        var projectId = ProjectId.NewId();
        var title = "Test Project Title";
        var description = "Test Project Description";
        var changedTitle = "Updated Project Title";
        var changedDescription = "Updated Project Description";
        var createdAt = new DateTimeOffset(2024, 1, 2, 3, 4, 5, TimeSpan.Zero);
        var changedAt = new DateTimeOffset(2024, 1, 3, 3, 4, 5, TimeSpan.Zero);
        this.Given(t => t.ProjectIsCrated(projectId, title, description, createdAt))
            .When(t => t.ProjectIsChanged(changedTitle, changedDescription, changedAt))
            .Then(t => t.InstanceContainsEvent(
                e => e.GetType() == typeof(ProjectDetailsChanged)
                    && ((ProjectDetailsChanged)e).ProjectId == projectId
                    && ((ProjectDetailsChanged)e).Title == changedTitle
                    && ((ProjectDetailsChanged)e).Description == changedDescription
                    && ((ProjectDetailsChanged)e).ChangedAt == changedAt,
                "ProjectDetailsChanged event was not raised with correct values."))
            .BDDfy();
    }

    private void ProjectIsCrated(
        ProjectId projectId,
        string title,
        string description,
        DateTimeOffset createdAt)
    {
        this.instance = ProjectAggregateRoot.Create(
            projectId,
            title,
            description,
            createdAt);
    }

    private void ProjectIsChanged(
        string newTitle,
        string newDescription,
        DateTimeOffset changedAt)
    {
        this.instance.ChangeDetails(newTitle, newDescription, changedAt);
    }

    private void InstanceContainsEvent(Predicate<object> predicate, string message)
    {
        this.instance.GetUncommitedEvents()
            .ShouldContain(e => predicate(e), message);
    }
}

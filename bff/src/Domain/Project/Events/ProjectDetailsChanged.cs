namespace ProjectFollowUp.BFF.Domain.Project.Events;

public record struct ProjectDetailsChanged(
    ProjectId ProjectId,
    string Title,
    string Description,
    DateTimeOffset ChangedAt) : IAggregateEvent;

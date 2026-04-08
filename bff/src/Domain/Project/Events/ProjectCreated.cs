namespace ProjectFollowUp.BFF.Domain.Project.Events;

using ProjectFollowUp.BFF.Domain.User;

public record struct ProjectCreated(
    ProjectId ProjectId,
    string Title,
    string Description,
    UserId CreatedBy,
    DateTimeOffset CreatedAt) : IAggregateEvent;

namespace ProjectFollowUp.BFF.Domain.Project.Events;

public record struct ProjectCreated(
    ProjectId ProjectId,
    string Title,
    string Description,
    DateTimeOffset CreatedAt);

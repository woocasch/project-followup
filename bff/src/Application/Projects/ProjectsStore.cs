namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Projects;
using ProjectFollowUp.BFF.Domain.Projects.ProjectEvents;

internal static class UsersStore
{
    private static readonly Collection<(ProjectId ProjectId, object DomainEvent)> projectEvents = [];

    static UsersStore()
    {
        var firstProjectId = ProjectId.FromGuid(Guid.NewGuid());
        object domainEvent = new ProjectCreated(firstProjectId, "First project", "Some description", DateTimeOffset.UtcNow);
        projectEvents.Add((ProjectId: firstProjectId, DomainEvent: domainEvent));
        var secondProjectId = ProjectId.FromGuid(Guid.NewGuid());
        domainEvent = new ProjectCreated(secondProjectId, "Second project", "Another description", DateTimeOffset.UtcNow);
        projectEvents.Add((ProjectId: secondProjectId, DomainEvent: domainEvent));
        domainEvent = new ProjectDetailsChanged(firstProjectId, "FirstProject", "Modified description", DateTimeOffset.UtcNow);
        projectEvents.Add((ProjectId: firstProjectId, DomainEvent: domainEvent));
    }

    public static void AddEvent(ProjectId projectId, object domainEvent)
    {
        projectEvents.Add((projectId, domainEvent));
    }

    public static ProjectAggregateRoot? GetProjectById(ProjectId projectId)
    {
        var events = projectEvents
            .Where(pe => pe.ProjectId == projectId)
            .Select(pe => pe.DomainEvent)
            .ToList();
        if (events.Count == 0)
        {
            return null;
        }

        var project = ProjectAggregateRoot.Rehydrate(events);
        return project;
    }

    public static IEnumerable<ProjectAggregateRoot> GetAllProjects()
    {
        var projectIds = projectEvents
            .Select(pe => pe.ProjectId)
            .Distinct()
            .ToList();
        return projectIds
            .Select(GetProjectById)
            .Where(project => project != null)!;
    }
}

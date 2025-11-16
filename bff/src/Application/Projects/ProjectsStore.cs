namespace ProjectFollowUp.BFF.Application.Projects;

using System.Collections.ObjectModel;

using ProjectFollowUp.BFF.Domain.Projects;

internal static class ProjectsStore
{
    private static readonly Collection<(ProjectId ProjectId, object DomainEvent)> projectEvents = [];

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
        foreach (var projectId in projectIds)
        {
            var project = GetProjectById(projectId);
            if (project != null)
            {
                yield return project;
            }
        }
    }
}

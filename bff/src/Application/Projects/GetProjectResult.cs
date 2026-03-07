namespace ProjectFollowUp.BFF.Application.Projects;

using ProjectFollowUp.BFF.Domain.Project;

public sealed class GetProjectResult
{
    private GetProjectResult(ProjectData? project)
    {
        this.Project = project;
    }

    public ProjectData? Project { get; }

    public bool ProjectExists => this.Project is not null;

    public static GetProjectResult NotFound() => new GetProjectResult(null);

    public static GetProjectResult Success(ProjectData project) => new GetProjectResult(project);

    public sealed class ProjectData(ProjectId projectId, string title, string description)
    {
        public ProjectId ProjectId { get; } = projectId;

        public string Title { get; } = title;

        public string Description { get; } = description;
    }
}

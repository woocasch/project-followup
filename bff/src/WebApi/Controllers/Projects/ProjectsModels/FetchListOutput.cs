namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects.ProjectsModels;

using System.Collections.ObjectModel;

public sealed class FetchListOutput(List<ProjectListItem> projects)
{
    public List<ProjectListItem> Projects { get; } = new([.. projects]);
}

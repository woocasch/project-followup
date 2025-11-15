namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using System.Collections.ObjectModel;

public class FetchListOutput
{
    public FetchListOutput(IEnumerable<ProjectListItem> projects)
    {
        this.Projects = new([.. projects]);
    }

    public ReadOnlyCollection<ProjectListItem> Projects { get; }
}

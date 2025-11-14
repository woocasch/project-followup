namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using System.Collections.ObjectModel;

public class FetchListResult
{
    public FetchListResult(IEnumerable<ProjectListItem> projects)
    {
        this.Projects = new([.. projects]);
    }

    public ReadOnlyCollection<ProjectListItem> Projects { get; }
}

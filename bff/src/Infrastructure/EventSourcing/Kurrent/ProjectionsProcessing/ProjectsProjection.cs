namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using KurrentDB.Client;

public sealed class ProjectsProjection(
    KurrentDBProjectionManagementClient client)
    : ProjectionBase(client, ProjectionName, typeof(ProjectsProjection).Assembly, ResourceName)
{
    public const string ProjectionName = "Project";

    private const string ResourceName = "ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing.Projects.js";
}

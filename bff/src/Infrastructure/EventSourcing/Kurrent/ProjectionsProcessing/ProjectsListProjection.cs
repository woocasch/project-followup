namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using KurrentDB.Client;

public sealed class ProjectsListProjection(
    KurrentDBProjectionManagementClient client)
    : ProjectionBase(client, ProjectionName, typeof(ProjectsListProjection).Assembly, ResourceName)
{
    public const string ProjectionName = "ProjectsList";

    private const string ResourceName = "ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing.ProjectsList.js";
}

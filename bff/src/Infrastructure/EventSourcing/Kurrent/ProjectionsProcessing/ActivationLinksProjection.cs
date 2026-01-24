namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using KurrentDB.Client;

public sealed class ActivationLinksProjection(
KurrentDBProjectionManagementClient client)
: ProjectionBase(client, ProjectionName, typeof(ProjectsListProjection).Assembly, ResourceName)
{
    public const string ProjectionName = "ActivationLinks";

    private const string ResourceName = "ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing.ActivationLinks.js";
}
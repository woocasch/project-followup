namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using KurrentDB.Client;

public sealed class UsersProjection(
    KurrentDBProjectionManagementClient client)
    : ProjectionBase(client, ProjectionName, typeof(UsersProjection).Assembly, ResourceName)
{
    private const string ProjectionName = "Users";

    private const string ResourceName = "ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing.Users.js";
}

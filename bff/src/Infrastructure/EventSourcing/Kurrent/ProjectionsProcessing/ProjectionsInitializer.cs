namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using System.Threading;
using System.Threading.Tasks;

public sealed class ProjectionsInitializer(
    IProjectionFactory projectionFactory) : IProjectionsInitializer
{
    public async Task InitializeProjections(CancellationToken cancellationToken)
    {
        var allProjections = projectionFactory.CreateAllProjections();
        foreach(var projection in allProjections)
        {
            await projection.CreateOrUpdate(cancellationToken);
        }
    }
}

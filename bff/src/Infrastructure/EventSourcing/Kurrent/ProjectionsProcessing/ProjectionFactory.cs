namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

public sealed class ProjectionFactory(
    IServiceProvider serviceProvider) : IProjectionFactory
{
    public IEnumerable<IProjection> CreateAllProjections()
    {
        return serviceProvider.GetServices<IProjection>();
    }
}

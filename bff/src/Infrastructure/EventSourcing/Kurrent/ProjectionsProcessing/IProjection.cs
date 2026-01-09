namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

public interface IProjection
{
    Task CreateOrUpdate(CancellationToken cancellationToken);
}

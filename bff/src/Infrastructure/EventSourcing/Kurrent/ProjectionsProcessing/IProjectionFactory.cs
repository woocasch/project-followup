namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

public interface IProjectionFactory
{
    IEnumerable<IProjection> CreateAllProjections();
}

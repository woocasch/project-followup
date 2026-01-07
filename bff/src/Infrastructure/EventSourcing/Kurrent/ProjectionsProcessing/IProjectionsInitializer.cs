namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.ProjectionsProcessing;

public interface IProjectionsInitializer
{
    Task InitializeProjections(CancellationToken cancellationToken);
}

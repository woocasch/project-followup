namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent.EventsMaterialization;

public interface IReadModelHydrator
{
    Task Subscribe(CancellationToken cancellationToken);
}

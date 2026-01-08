namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

public interface INamingService
{
    string GetStreamName<TAggregateType>(Guid aggregateId)
        where TAggregateType : class;
}

namespace ProjectFollowUp.BFF.Infrastructure.EventSourcing.Kurrent;

public interface INamingService
{
    string GetStreamName<TAggregateType>(string aggregateId)
        where TAggregateType : class;
}

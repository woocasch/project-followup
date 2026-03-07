namespace ProjectFollowUp.BFF.Application.Cqrs;

using Microsoft.Extensions.Logging;

public abstract class QueryHandlerBase<TQuery, TResult>(
    ILogger<QueryHandlerBase<TQuery, TResult>> logger)
    : IQueryHandler<TResult>, IQueryHandlerBase
    where TQuery : IQuery<TResult>
    where TResult : notnull
{
    public async Task<TResult> Handle(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        logger.HandleStarted(query.GetType());
        if (query is not TQuery typedQuery)
        {
            logger.InvalidQueryType(typeof(TQuery), query.GetType());
            var message = $"Invalid query type. Expected: {typeof(TQuery).FullName}, Actual: {query.GetType().FullName}";
            throw new InvalidOperationException(message);
        }

        logger.PassingToStronglyTypedHandle(query.GetType());
        try
        {
            var result = await this.HandleQuery(typedQuery, cancellationToken);
            logger.Completed(query.GetType());
            return result;
        }
        catch (Exception ex)
        {
            logger.ExceptionOccurred(query.GetType(), ex);
            throw;
        }
    }

    protected abstract Task<TResult> HandleQuery(TQuery query, CancellationToken cancellationToken);
}

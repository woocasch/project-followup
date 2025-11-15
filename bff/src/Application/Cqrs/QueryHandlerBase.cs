namespace ProjectFollowUp.BFF.Application.Cqrs;

public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TResult>
    where TQuery : IQuery<TResult>
    where TResult : notnull
{
    public async Task<TResult?> Handle(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        if (query is not TQuery typedQuery)
        {
            var message = $"Invalid query type. Expected: {typeof(TQuery).FullName}, Actual: {query.GetType().FullName}";
            throw new InvalidOperationException(message);
        }

        return await this.HandleQuery(typedQuery, cancellationToken);
    }

    protected abstract Task<TResult?> HandleQuery(TQuery query, CancellationToken cancellationToken);
}

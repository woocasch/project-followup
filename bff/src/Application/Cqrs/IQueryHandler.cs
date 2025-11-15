namespace ProjectFollowUp.BFF.Application.Cqrs;


public interface IQueryHandler<TResult>
    where TResult : notnull
{
    Task<TResult?> Handle(IQuery<TResult> query, CancellationToken cancellationToken);
}

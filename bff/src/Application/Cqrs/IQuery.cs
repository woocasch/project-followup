namespace ProjectFollowUp.BFF.Application.Cqrs;

public interface IQuery<TResult>
    where TResult : notnull
{
}

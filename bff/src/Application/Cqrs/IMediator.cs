namespace ProjectFollowUp.BFF.Application.Cqrs;

public interface IMediator
{
    Task<CommandResult> Send(ICommand command, CancellationToken cancellationToken);

    Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : notnull;
}

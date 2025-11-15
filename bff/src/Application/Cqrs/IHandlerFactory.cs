namespace ProjectFollowUp.BFF.Application.Cqrs;

public interface IHandlerFactory
{
    ICommandHandler? CreateCommandHandler(ICommand command);

    IQueryHandler<TResult>? CreateQueryHandler<TResult>(IQuery<TResult> query)
        where TResult : notnull;
}

namespace ProjectFollowUp.BFF.Application.Cqrs;

public static class NamingConventions
{
    public static string CommandHandlerName(ICommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        return command.GetType().FullName!;
    }

    public static string CommandHandlerName<TCommand>()
        where TCommand : ICommand
    {
        return typeof(TCommand).FullName!;
    }
    public static string QueryHandlerName<TResult>(IQuery<TResult> query)
        where TResult : notnull
    {
        ArgumentNullException.ThrowIfNull(query);

        return query.GetType().FullName!;
    }

    public static string QueryHandlerName<TQuery, TResult>()
        where TResult : notnull
        where TQuery : IQuery<TResult>
    {
        return typeof(TQuery).FullName!;
    }
}

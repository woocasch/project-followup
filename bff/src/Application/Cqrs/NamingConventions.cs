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
}

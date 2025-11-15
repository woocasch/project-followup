namespace ProjectFollowUp.BFF.Application.Cqrs;

public sealed class CommandResult
{
    private CommandResult(string errorCode, Exception? exception)
    {
        this.ErrorCode = errorCode;
        this.Exception = exception;
    }

    public string ErrorCode { get; }

    public Exception? Exception { get; }

    public bool IsFatalError => this.Exception is not null;

    public bool IsSuccess => string.IsNullOrEmpty(this.ErrorCode);

    public static CommandResult Success() => new CommandResult(string.Empty, null);

    public static CommandResult Failure(string errorCode) => new CommandResult(errorCode, null);

    public static CommandResult FatalError(Exception exception) => new CommandResult("FatalError", exception);
}

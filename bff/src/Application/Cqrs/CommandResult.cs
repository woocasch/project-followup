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

    public bool IsSuccess => string.IsNullOrEmpty(this.ErrorCode);

    public static CommandResult Success() => new(string.Empty, null);

    public static CommandResult Failure(string errorCode) => new(errorCode, null);

    public static CommandResult Failure(string errorCode, Exception exception) => new(errorCode, exception);
}

namespace ProjectFollowUp.BFF.WebApi.Validation;

public sealed class ValidationErrorsDetailsEntry
{
    public ValidationErrorsDetailsEntry(
        string propertyName, 
        string code,
        string message)
    {
        this.PropertyName = propertyName;
        this.Code = code;
        this.Message = message;
    }

    public string PropertyName { get; }

    public string Code { get; }

    public string Message { get; }
}

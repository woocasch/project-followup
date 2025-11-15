namespace ProjectFollowUp.BFF.WebApi.Validation;

using System.Collections.ObjectModel;

public class ValidationErrorsDetails
{
    public ValidationErrorsDetails(
        IEnumerable<ValidationErrorsDetailsEntry> errors)
    {
        this.Errors = new ReadOnlyCollection<ValidationErrorsDetailsEntry>(errors.ToList());
    }

    public ReadOnlyCollection<ValidationErrorsDetailsEntry> Errors { get; }
}

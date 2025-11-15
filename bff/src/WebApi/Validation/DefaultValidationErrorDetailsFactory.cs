namespace ProjectFollowUp.BFF.WebApi.Validation;

using System.Collections.Generic;

using FluentValidation.Results;

public sealed class DefaultValidationErrorDetailsFactory : IValidationErrorsDetailsFactory
{
    public ValidationErrorsDetails CreateFromValidationResults(IEnumerable<ValidationResult> validationResults)
    {
        return new ValidationErrorsDetails(CreateEntries(validationResults));
    }

    private IEnumerable<ValidationErrorsDetailsEntry> CreateEntries(IEnumerable<ValidationResult> failures)
    {
        return failures.SelectMany(CreateEntries);
    }

    private static IEnumerable<ValidationErrorsDetailsEntry> CreateEntries(ValidationResult failures)
    {
        return failures.Errors.Select(CreateEntry);
    }

    private static ValidationErrorsDetailsEntry CreateEntry(ValidationFailure failure)
    {
        return new ValidationErrorsDetailsEntry(
            failure.PropertyName,
            failure.ErrorCode,
            failure.ErrorMessage);
    }
}

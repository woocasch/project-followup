namespace ProjectFollowUp.BFF.WebApi.Validation;

using FluentValidation.Results;

public interface IValidationErrorsDetailsFactory
{
    ValidationErrorsDetails CreateFromValidationResults(IEnumerable<ValidationResult> validationResults);
}

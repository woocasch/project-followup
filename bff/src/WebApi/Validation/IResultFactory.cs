namespace ProjectFollowUp.BFF.WebApi.Validation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public interface IResultFactory
{
    IActionResult CreateValidationResult(ActionExecutingContext context, ValidationErrorsDetails validationErrorsDetails);
}

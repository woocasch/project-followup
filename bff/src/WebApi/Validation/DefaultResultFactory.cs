namespace ProjectFollowUp.BFF.WebApi.Validation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public sealed class DefaultResultFactory : IResultFactory
{
    public IActionResult CreateValidationResult(ActionExecutingContext context, ValidationErrorsDetails validationErrorsDetails)
    {
        return new BadRequestObjectResult(validationErrorsDetails);
    }
}

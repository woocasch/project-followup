namespace ProjectFollowUp.BFF.WebApi.Validation;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IResultFactory resultFactory;

    private readonly IValidationErrorsDetailsFactory validationErrorsDetailsFactory;

    public ValidationFilter(
        IResultFactory resultFactory,
        IValidationErrorsDetailsFactory validationErrorsDetailsFactory)
    {
        this.resultFactory = resultFactory;
        this.validationErrorsDetailsFactory = validationErrorsDetailsFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!IsControllerSupported(context.Controller))
        {
            await next();
            return;
        }

        var serviceProvider = context.HttpContext.RequestServices;
        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        var results = new Collection<ValidationResult>();
        foreach (var parameter in controllerActionDescriptor.Parameters)
        {
            if (!context.ActionArguments.TryGetValue(parameter.Name!, out var parameterValue))
            {
                continue;
            }

            var parameterInfo = (parameter as ControllerParameterDescriptor)?.ParameterInfo;
            var parameterType = parameterValue?.GetType();

            if (parameterValue is null || parameterType is null)
            {
                continue;
            }

            var validator = serviceProvider.GetService(typeof(IValidator<>).MakeGenericType(parameterType)) as IValidator;
            if (validator is null)
            {
                continue;
            }

            IValidationContext validationContext = new ValidationContext<object>(parameterValue);
            var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);
            if (!validationResult.IsValid)
            {
                results.Add(validationResult);
                foreach (var error in validationResult.Errors)
                {
                    context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
        }

        if (results.Count > 0)
        {
            var validationErrorsDetails = this.validationErrorsDetailsFactory.CreateFromValidationResults(results);
            context.Result = this.resultFactory.CreateValidationResult(context, validationErrorsDetails);
            return;
        }

        await next();
    }

    private static bool IsControllerSupported(object controller)
    {
        if (!(controller is ControllerBase))
        {
            return false;
        }

        var controllerType = controller.GetType();
        var hasApiControllerAttribute = controllerType
            .GetCustomAttributes(typeof(ApiControllerAttribute), inherit: true)
            .Any();
        if (!hasApiControllerAttribute)
        {
            return false;
        }

        return true;
    }
}

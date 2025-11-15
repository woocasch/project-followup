namespace ProjectFollowUp.BFF.WebApi.Validation;

using Microsoft.AspNetCore.Mvc;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidation(
        this IServiceCollection services)
    {
        services.AddScoped<IResultFactory, DefaultResultFactory>();
        services.AddScoped<IValidationErrorsDetailsFactory, DefaultValidationErrorDetailsFactory>();
        services.AddScoped<ValidationFilter>();
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.AddService<ValidationFilter>(order: int.MinValue);
        });
        return services;
    }
}

namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using System.Text.RegularExpressions;

using FluentValidation;

public sealed partial class UpdateInputValidator : AbstractValidator<UpdateInput>
{
    private static readonly Regex TitleRegex = TitleValidationRegex();

    private static readonly Regex DescriptionRegex = DescriptionValidationRegex();

    public UpdateInputValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithErrorCode("Required")
            .NotEmpty().WithErrorCode("Required")
            .MaximumLength(100).WithErrorCode("MaxLength100")
            .Matches(TitleRegex).WithErrorCode("ForbiddenCharacters");

        RuleFor(x => x.Description)
            .NotNull().WithErrorCode("Required")
            .NotEmpty().WithErrorCode("Required")
            .MaximumLength(500).WithErrorCode("MaxLength500")
            .Matches(DescriptionRegex).WithErrorCode("ForbiddenCharacters");
    }

    // Allow letters (including accents), marks, numbers and punctuation. Do not allow symbols (\p{S}) or line breaks.
    [GeneratedRegex(@"^[\p{L}\p{M}\p{N}\p{P} ]+$", RegexOptions.Compiled)]
    private static partial Regex TitleValidationRegex();

    [GeneratedRegex(@"^[\p{L}\p{M}\p{N}\p{P}\s]+$", RegexOptions.Compiled)]
    private static partial Regex DescriptionValidationRegex();
}

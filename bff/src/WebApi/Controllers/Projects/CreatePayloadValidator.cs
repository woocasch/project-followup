namespace ProjectFollowUp.BFF.WebApi.Controllers.Projects;

using System.Text.RegularExpressions;
using FluentValidation;

public sealed partial class CreatePayloadValidator : AbstractValidator<CreatePayload>
{
    private static readonly Regex TitleRegex = TitleValidationRegex();

    public CreatePayloadValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithErrorCode("Required")
            .NotEmpty().WithErrorCode("Required")
            .MaximumLength(100).WithErrorCode("MaxLength100")
            .Matches(TitleRegex).WithErrorCode("ForbiddenCharacters");

        RuleFor(x => x.Description)
            .NotNull().WithErrorCode("Required")
            .NotEmpty().WithErrorCode("Required")
            .MaximumLength(500).WithErrorCode("MaxLength500");
    }

    // Allow letters (including accents), marks, numbers and punctuation. Do not allow symbols (\p{S}) or line breaks.
    [GeneratedRegex("^[\\p{L}\\p{M}\\p{N}\\p{P} ]+$", RegexOptions.Compiled)]
    private static partial Regex TitleValidationRegex();
}

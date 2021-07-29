using Application.CQRS.Ads.Commands;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands
{
    public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
    {
        public CreateAdCommandValidator()
        {
            RuleFor(c => c.Cost)
                .GreaterThanOrEqualTo(AdValidationOptions.CostMinValue);

            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(AdValidationOptions.ContentMinLength)
                .MaximumLength(AdValidationOptions.ContentMaxLength);
        }
    }
}
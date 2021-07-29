using Application.CQRS.Ads.Commands.AdDisabled;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdDisabled
{
    public class DisableAdCommandValidator : AbstractValidator<DisableAdCommand>
    {
        public DisableAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();
        }
    }
}
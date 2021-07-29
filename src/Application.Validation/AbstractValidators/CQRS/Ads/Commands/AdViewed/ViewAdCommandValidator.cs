using Application.CQRS.Ads.Commands.AdViewed;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdViewed
{
    public class ViewAdCommandValidator : AbstractValidator<ViewAdCommand>
    {
        public ViewAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();
        }
    }
}
using Application.CQRS.Ads.Commands.AdTag;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdTag
{
    public class AddTagToAdCommandValidator : AbstractValidator<AddTagToAdCommand>
    {
        public AddTagToAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();

            RuleFor(c => c.TagId)
                .NotEmpty();
        }
    }
}
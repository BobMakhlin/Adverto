using Application.CQRS.Ads.Commands.AdTag;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdTag
{
    public class DeleteTagOfAdCommandValidator : AbstractValidator<DeleteTagOfAdCommand>
    {
        public DeleteTagOfAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();

            RuleFor(c => c.TagId)
                .NotEmpty();
        }
    }
}
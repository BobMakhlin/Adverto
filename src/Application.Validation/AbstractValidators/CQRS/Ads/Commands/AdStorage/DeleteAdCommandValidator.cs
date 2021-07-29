using Application.CQRS.Ads.Commands.AdStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage
{
    public class DeleteAdCommandValidator : AbstractValidator<DeleteAdCommand>
    {
        public DeleteAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();
        }        
    }
}
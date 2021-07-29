using Application.CQRS.Ads.Commands.AdCategory;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdCategory
{
    public class DeleteCategoryOfAdCommandValidator : AbstractValidator<DeleteCategoryOfAdCommand>
    {
        public DeleteCategoryOfAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();
            
            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}
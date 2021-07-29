using Application.CQRS.Ads.Commands.AdCategory;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdCategory
{
    public class AddCategoryToAdCommandValidator : AbstractValidator<AddCategoryToAdCommand>
    {
        public AddCategoryToAdCommandValidator()
        {
            RuleFor(c => c.AdId)
                .NotEmpty();
            
            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}
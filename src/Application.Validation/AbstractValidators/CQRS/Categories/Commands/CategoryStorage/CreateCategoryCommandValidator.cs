using Application.CQRS.Categories.Commands.CategoryStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Commands.CategoryStorage
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IAdvertoDbContext context)
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(CategoryValidationOptions.TitleMinLength, CategoryValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Categories, c => c.Title);
        }
    }
}
using Application.CQRS.Ads.Commands.AdStorage.Create.Abstractions;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Create
{
    public class CreateAdCommandBaseValidator : AbstractValidator<CreateAdCommandBase>
    {
        public CreateAdCommandBaseValidator(IAdvertoDbContext context)
        {
            RuleFor(c => c.Cost)
                .GreaterThanOrEqualTo(AdCommonValidationOptions.CostMinValue);

            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(AdCommonValidationOptions.ContentMinLength)
                .MaximumLength(AdCommonValidationOptions.ContentMaxLength)
                .UniqueInsideOfDbSetColumn(context.Ads, ad => ad.Content);

            RuleFor(c => c.CategoryIds)
                .NotEmpty()
                .ArrayMinimumLength(AdCommonValidationOptions.CategoryIdsMinLength);
        }
    }
}
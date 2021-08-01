using Application.CQRS.Ads.Commands.AdStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using Domain.Primary.Entities;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage
{
    public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
    {
        public CreateAdCommandValidator(IAdvertoDbContext context)
        {
            RuleFor(c => c.Cost)
                .GreaterThanOrEqualTo(AdValidationOptions.CostMinValue);

            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(AdValidationOptions.ContentMinLength)
                .MaximumLength(AdValidationOptions.ContentMaxLength)

                .UniqueInsideOfDbSetColumn(context.Ads, ad => ad.Content)

                .UrlContentTypeIsOneOf(AdValidationOptions.BannerAdUrlAllowedContentTypes)
                .When(c => c.AdType == AdType.BannerAd, ApplyConditionTo.CurrentValidator);

            RuleFor(c => c.CategoryIds)
                .NotEmpty()
                .ArrayMinimumLength(AdValidationOptions.CategoryIdsMinLength);
        }
    }
}
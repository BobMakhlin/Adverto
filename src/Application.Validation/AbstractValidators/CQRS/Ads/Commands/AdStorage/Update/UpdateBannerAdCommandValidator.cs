using Application.CQRS.Ads.Commands.AdStorage.Update.Realisations;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Update
{
    public class UpdateBannerAdCommandValidator : AbstractValidator<UpdateBannerAdCommand>
    {
        public UpdateBannerAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new UpdateAdCommandBaseValidator(context));

            RuleFor(c => c.Content)
                .UrlContentTypeIsOneOf(BannerAdValidationOptions.UrlAllowedContentTypes);
        }
    }
}
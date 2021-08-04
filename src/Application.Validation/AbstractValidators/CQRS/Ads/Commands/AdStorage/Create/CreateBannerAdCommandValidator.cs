using Application.CQRS.Ads.Commands.AdStorage.Create.Realisations;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Create
{
    public class CreateBannerAdCommandValidator : AbstractValidator<CreateBannerAdCommand>
    {
        public CreateBannerAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new CreateAdCommandBaseValidator(context));

            RuleFor(c => c.Content)
                .UrlContentTypeIsOneOf(BannerAdValidationOptions.UrlAllowedContentTypes);
        }
    }
}
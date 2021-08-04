using Application.CQRS.Ads.Commands.AdStorage.Update.Realisations;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Update
{
    public class UpdateVideoAdCommandValidator : AbstractValidator<UpdateVideoAdCommand>
    {
        public UpdateVideoAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new UpdateAdCommandBaseValidator(context));
            
            RuleFor(c => c.Content)
                .UrlContentTypeIsOneOf(VideoAdValidationOptions.UrlAllowedContentTypes);
        }
    }
}
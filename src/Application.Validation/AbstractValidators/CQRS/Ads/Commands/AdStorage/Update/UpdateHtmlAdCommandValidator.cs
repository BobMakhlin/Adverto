using Application.CQRS.Ads.Commands.AdStorage.Update.Realisations;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using Application.Validation.Tools.Helpers;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Update
{
    public class UpdateHtmlAdCommandValidator : AbstractValidator<UpdateHtmlAdCommand>
    {
        public UpdateHtmlAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new UpdateAdCommandBaseValidator(context));

            When(CommandContentIsValidUrl, () =>
            {
                RuleFor(c => c.Content)
                    .UrlContentTypeIsOneOf(HtmlAdValidationOptions.UrlAllowedContentTypes);
            }).Otherwise(() =>
            {
                RuleFor(c => c.Content)
                    .ValidHtmlMarkup();
            });
        }
        
        /// <summary>
        /// Checks if the property <see cref="UpdateHtmlAdCommand.Content"/> of
        /// parameter <paramref name="command"/> is a valid url.
        /// </summary>
        private bool CommandContentIsValidUrl(UpdateHtmlAdCommand command)
            => UrlValidationHelpers.IsStringValidUrl(command.Content);
    }
}
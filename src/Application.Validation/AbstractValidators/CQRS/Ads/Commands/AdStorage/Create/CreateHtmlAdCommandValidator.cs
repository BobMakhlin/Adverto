using Application.CQRS.Ads.Commands.AdStorage.Create.Realisations;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using Application.Validation.Tools.Helpers;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Create
{
    public class CreateHtmlAdCommandValidator : AbstractValidator<CreateHtmlAdCommand>
    {
        public CreateHtmlAdCommandValidator(IAdvertoDbContext context)
        {
            Include(new CreateAdCommandBaseValidator(context));

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
        /// Checks if the property <see cref="CreateHtmlAdCommand.Content"/> of
        /// parameter <paramref name="command"/> is a valid url.
        /// </summary>
        private bool CommandContentIsValidUrl(CreateHtmlAdCommand command)
            => UrlValidationHelpers.IsStringValidUrl(command.Content);
    }
}
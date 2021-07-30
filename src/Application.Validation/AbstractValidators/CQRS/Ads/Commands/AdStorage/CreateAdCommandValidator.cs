using System.Collections.Generic;
using System.Net.Mime;
using Application.CQRS.Ads.Commands.AdStorage;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using Domain.Primary.Entities;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage
{
    public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
    {
        #region Fields

        /// <summary>
        /// The collection, containing image MIME-types (e.g., gif, jpeg).
        /// </summary>
        private static readonly IEnumerable<string> ImageMimeTypes = new[]
            {MediaTypeNames.Image.Gif, MediaTypeNames.Image.Jpeg};
        
        #endregion

        #region Constructors

        public CreateAdCommandValidator()
        {
            RuleFor(c => c.Cost)
                .GreaterThanOrEqualTo(AdValidationOptions.CostMinValue);

            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(AdValidationOptions.ContentMinLength)
                .MaximumLength(AdValidationOptions.ContentMaxLength)
                
                .UrlContentTypeIsOneOf(ImageMimeTypes)
                .When(c => c.AdType == AdType.BannerAd, ApplyConditionTo.CurrentValidator);
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using Domain.Primary.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage
{
    public class UpdateAdCommandValidator : AbstractValidator<UpdateAdCommand>
    {
        #region Fields

        private readonly IAdvertoDbContext _context;
        
        /// <summary>
        /// The collection, containing image MIME-types (e.g., gif, jpeg).
        /// </summary>
        private static readonly IEnumerable<string> ImageMimeTypes = new[]
            {MediaTypeNames.Image.Gif, MediaTypeNames.Image.Jpeg};

        #endregion

        #region Constructors

        public UpdateAdCommandValidator(IAdvertoDbContext context)
        {
            _context = context;
            RuleFor(c => c.Cost)
                .GreaterThanOrEqualTo(AdValidationOptions.CostMinValue);

            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(AdValidationOptions.ContentMinLength)
                .MaximumLength(AdValidationOptions.ContentMaxLength)
                
                .UniqueInsideOfDbSetColumn(context.Ads, ad => ad.Content)
                .WhenAsync(AdContentWasUpdated, ApplyConditionTo.CurrentValidator)
                
                .UrlContentTypeIsOneOf(ImageMimeTypes)
                .When(c => c.AdType == AdType.BannerAd, ApplyConditionTo.CurrentValidator);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the ad content was changed in the current update.
        /// </summary>
        /// <param name="command">
        /// Contains information about the current update of the ad.
        /// </param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> AdContentWasUpdated(UpdateAdCommand command, CancellationToken token)
        {
            string currentAdContent = await _context.Ads
                .Where(c => c.AdId == command.AdId)
                .Select(c => c.Content)
                .SingleOrDefaultAsync(token);
            return currentAdContent != command.Content;
        }

        #endregion
    }
}
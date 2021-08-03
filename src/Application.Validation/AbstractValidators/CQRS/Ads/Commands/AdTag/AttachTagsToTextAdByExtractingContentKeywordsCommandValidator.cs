using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdTag;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdTag
{
    public class
        AttachTagsToTextAdByExtractingContentKeywordsCommandValidator : AbstractValidator<
            AttachTagsToTextAdByExtractingContentKeywordsCommand>
    {
        #region Fields

        private readonly IAdvertoDbContext _context;

        #endregion

        #region Constructors

        public AttachTagsToTextAdByExtractingContentKeywordsCommandValidator(IAdvertoDbContext context)
        {
            _context = context;

            RuleFor(c => c.AdId)
                .MustAsync(BeOfTypeTextAd)
                .WithMessage("The ad must be of type 'TextAd'");
        }

        #endregion

        #region Methods

        private async Task<bool> BeOfTypeTextAd(Guid adId, CancellationToken token)
        {
            AdType adType = await _context.Ads
                .Where(ad => ad.AdId == adId)
                .Select(ad => ad.AdType)
                .SingleOrDefaultAsync(token)
                .ConfigureAwait(false);
            return adType == AdType.TextAd;
        }

        #endregion
    }
}
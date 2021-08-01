using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdCategory;
using Application.Persistence.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdCategory
{
    public class DeleteCategoryOfAdCommandValidator : AbstractValidator<DeleteCategoryOfAdCommand>
    {
        #region Fields

        private readonly IAdvertoDbContext _context;

        #endregion
        
        #region Constructors

        public DeleteCategoryOfAdCommandValidator(IAdvertoDbContext context)
        {
            _context = context;
            
            RuleFor(c => c.AdId)
                .NotEmpty()
                .MustAsync(HaveMoreThanOneCategory)
                .WithMessage
                    ("You cannot remove the last category of the ad. The ad must have at least one category.");

            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the ad with the specified <paramref name="adId"/> has more than one category.
        /// </summary>
        private async Task<bool> HaveMoreThanOneCategory(Guid adId, CancellationToken token)
        {
            int adCategoriesCount = await _context.Ads
                .Where(ad => ad.AdId == adId)
                .Select(ad => ad.Categories.Count())
                .SingleOrDefaultAsync(token);
            return adCategoriesCount > 1;
        }

        #endregion
    }
}
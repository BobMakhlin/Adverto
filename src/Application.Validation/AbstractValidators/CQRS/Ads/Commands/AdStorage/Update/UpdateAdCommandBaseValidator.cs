using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Commands.AdStorage.Update.Abstractions;
using Application.Persistence.Interfaces;
using Application.Validation.Options.Ad;
using Application.Validation.Tools.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Ads.Commands.AdStorage.Update
{
    public class UpdateAdCommandBaseValidator : AbstractValidator<UpdateAdCommandBase>
    {
        #region Fields

        private readonly IAdvertoDbContext _context;

        #endregion

        #region Constructors

        public UpdateAdCommandBaseValidator(IAdvertoDbContext context)
        {
            _context = context;
            
            RuleFor(c => c.Cost)
                .GreaterThanOrEqualTo(AdCommonValidationOptions.CostMinValue);

            RuleFor(c => c.Content)
                .NotEmpty()
                .MinimumLength(AdCommonValidationOptions.ContentMinLength)
                .MaximumLength(AdCommonValidationOptions.ContentMaxLength)
                
                .UniqueInsideOfDbSetColumn(context.Ads, ad => ad.Content)
                .WhenAsync(AdContentWasUpdated, ApplyConditionTo.CurrentValidator);
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
        private async Task<bool> AdContentWasUpdated(UpdateAdCommandBase command, CancellationToken token)
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
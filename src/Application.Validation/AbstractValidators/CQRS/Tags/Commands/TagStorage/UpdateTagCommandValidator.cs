using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Tags.Commands.TagStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Tags.Commands.TagStorage
{
    public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
    {
        #region Fields

        private readonly IAdvertoDbContext _context;

        #endregion
        
        #region Constructors

        public UpdateTagCommandValidator(IAdvertoDbContext context)
        {
            _context = context;
            
            RuleFor(c => c.TagId)
                .NotEmpty();
            
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(TagValidationOptions.TitleMinLength, TagValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Tags, c => c.Title)
                .WhenAsync(TagTitleWasUpdated, ApplyConditionTo.CurrentValidator);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the tag title was changed in the current update.
        /// </summary>
        /// <param name="command">
        /// Contains information about the current update of the tag.
        /// </param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> TagTitleWasUpdated(UpdateTagCommand command, CancellationToken token)
        {
            string currentTagTitle = await _context.Tags
                .Where(c => c.TagId == command.TagId)
                .Select(c => c.Title)
                .SingleOrDefaultAsync(token);
            return currentTagTitle != command.Title;
        } 

        #endregion
    }
}
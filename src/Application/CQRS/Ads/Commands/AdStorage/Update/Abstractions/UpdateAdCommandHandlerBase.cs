using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.Ads.Commands.AdStorage.Update.Abstractions
{
    /// <summary>
    /// The base class for all ad-update command handlers.
    /// </summary>
    public abstract class UpdateAdCommandHandlerBase
    {
        #region Fields

        protected readonly IAdvertoDbContext _context;

        #endregion

        #region Constructors

        protected UpdateAdCommandHandlerBase(IAdvertoDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates an ad using properties of the <paramref name="command"/> parameter.
        /// It sends the changes to the database, so there is no need to call
        /// <see cref="IAdvertoDbContext.SaveChangesAsync(CancellationToken)"/>.
        /// </summary>
        protected async Task UpdateAdAsync(UpdateAdCommandBase command, CancellationToken cancellationToken)
        {
            Ad ad = await _context.Ads.FindAsync(command.AdId)
                        .ConfigureAwait(false)
                    ?? throw new NotFoundException(nameof(Ad), command.AdId);

            UpdateAdProperties(ad, command);
            
            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Updates <paramref name="ad"/> properties using the <paramref name="command"/> parameter.
        /// </summary>
        /// <param name="ad">An object which properties will be updated</param>
        /// <param name="command">An object that contains new properties values for <paramref name="ad"/> parameter</param>
        private void UpdateAdProperties(Ad ad, UpdateAdCommandBase command)
        {
            ad.AdType = command.AdType;
            ad.Cost = command.Cost;
            ad.Content = command.Content;
        }

        #endregion
    }
}
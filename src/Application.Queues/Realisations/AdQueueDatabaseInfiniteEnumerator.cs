using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Application.Queues.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queues.Realisations
{
    /// <summary>
    /// The infinite enumerator of the ad queue, that internally works with the database.
    /// When the enumerator makes move, standing on the last ad, it will start from the beginning.
    /// </summary>
    public class AdQueueDatabaseInfiniteEnumerator : IAdQueueEnumerator
    {
        #region Fields

        private readonly IAdvertoDbContext _context;

        #endregion

        #region Constructors

        public AdQueueDatabaseInfiniteEnumerator(IAdvertoDbContext context)
        {
            _context = context;
        }

        #endregion

        #region IAdQueueEnumerator

        /// <summary>
        /// Don't call this method - there is nothing to dispose in this class.
        /// </summary>
        public ValueTask DisposeAsync() => new ValueTask();

        public async ValueTask<bool> MoveNextAsync()
        {
            AdQueue adQueueInfo = await GetCurrentAdQueueInfoAsync()
                .ConfigureAwait(false);
            Current = await FindAdIdByIndexAsync(adQueueInfo.CurrentAdIndex)
                .ConfigureAwait(false);

            bool isNeedToResetQueue = await IsNeedToResetQueueAsync(adQueueInfo)
                .ConfigureAwait(false);
            adQueueInfo.CurrentAdIndex = isNeedToResetQueue ? 0 : (adQueueInfo.CurrentAdIndex + 1);

            await _context.SaveChangesAsync()
                .ConfigureAwait(false);

            return true;
        }

        public Guid Current { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the info about current ad-queue in the database.
        /// </summary>
        /// <returns>
        /// The object of type <see cref="AdQueue"/>, containing the information about the current ad-queue.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// When the database doesn't have the information about the current ad-queue.
        /// </exception>
        private async Task<AdQueue> GetCurrentAdQueueInfoAsync()
        {
            return await _context.AdQueues
                       .FirstOrDefaultAsync()
                       .ConfigureAwait(false)
                   ?? throw new InvalidOperationException(
                       "The info about current ad-queue was not found in the database");
        }

        /// <summary>
        /// Finds the ad-id by the specified <paramref name="adIndex"/>.
        /// </summary>
        private async Task<Guid> FindAdIdByIndexAsync(int adIndex)
        {
            return await _context.Ads
                .OrderBy(ad => ad.AdId)
                .Skip(adIndex)
                .Select(ad => ad.AdId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Checks if there is need to reset (restart) the queue of the ads
        /// </summary>
        /// <param name="adQueueInfo">
        /// Contains information about the queue.
        /// </param>
        private async Task<bool> IsNeedToResetQueueAsync(AdQueue adQueueInfo)
        {
            int totalAdsCount = await _context.Ads.CountAsync()
                .ConfigureAwait(false);

            int lastAdIndex = totalAdsCount - 1;
            return adQueueInfo.CurrentAdIndex == lastAdIndex;
        }

        #endregion
    }
}
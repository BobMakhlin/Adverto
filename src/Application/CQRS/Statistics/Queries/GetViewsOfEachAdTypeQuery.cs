using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Statistics.Queries
{
    public class GetViewsOfEachAdTypeQuery : IRequest<Dictionary<AdType, int>>
    {
        #region Classes

        public class Handler : IRequestHandler<GetViewsOfEachAdTypeQuery, Dictionary<AdType, int>>
        {
            #region Fields

            private readonly IAdvertoDbContext _context;

            #endregion

            #region Constructors

            public Handler(IAdvertoDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<GetViewsOfEachAdTypeQuery, Dictionary<AdType, int>>

            public async Task<Dictionary<AdType, int>> Handle(GetViewsOfEachAdTypeQuery request,
                CancellationToken cancellationToken)
            {
                Dictionary<AdType, int> viewsGroupedByAdTypes =
                    await GetViewsOfEachAdTypeViewedAtLeastOnceAsync(cancellationToken)
                        .ConfigureAwait(false);

                IEnumerable<AdType> allExistingAdTypes = GetAllExistingAdTypes();
                IEnumerable<AdType> usedAdTypes = viewsGroupedByAdTypes.Select(x => x.Key);
                IEnumerable<AdType> unusedAdTypes = allExistingAdTypes.Except(usedAdTypes);

                foreach (AdType unusedAdType in unusedAdTypes)
                {
                    int countOfViews = 0;
                    viewsGroupedByAdTypes.Add(unusedAdType, countOfViews);
                }

                return viewsGroupedByAdTypes;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns the dictionary where the key is <see cref="AdType"/> and the value is views count.
            /// Note that this dictionary doesn't contain the ad-types, having zero views.
            /// </summary>
            private async Task<Dictionary<AdType, int>> GetViewsOfEachAdTypeViewedAtLeastOnceAsync(
                CancellationToken cancellationToken)
            {
                return await _context.AdViews
                    .AsNoTracking()
                    .Include(adView => adView.Ad)
                    .GroupBy(adView => adView.Ad.AdType)
                    .Select(group => new
                    {
                        group.Key,
                        Count = group.Count()
                    })
                    .ToDictionaryAsync
                    (
                        x => x.Key,
                        x => x.Count,
                        cancellationToken
                    )
                    .ConfigureAwait(false);
            }
            
            /// <summary>
            /// Returns the collection of all existing ad-types.
            /// </summary>
            private IEnumerable<AdType> GetAllExistingAdTypes() => (AdType[]) Enum.GetValues(typeof(AdType));

            #endregion
        }

        #endregion
    }
}
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

            #endregion
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Ads.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using Domain.Primary.Entities;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ads.Queries.AdStorage
{
    public class FilterAdsQuery : IRequest<List<AdDto>>
    {
        #region Properties

        public AdType? AdType { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> TagIds { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<FilterAdsQuery, List<AdDto>>
        {
            #region Fields

            private readonly IAdvertoDbContext _context;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IAdvertoDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<FilterAdsQuery, List<AdDto>>

            public async Task<List<AdDto>> Handle(FilterAdsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Ad, bool>> predicate = GetFilterPredicate(request);

                return await _context.Ads
                    .AsNoTracking()
                    .Where(predicate)
                    .ProjectToListAsync<AdDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Builds the predicate based on the properties of <paramref name="request"/> parameter.
            /// </summary>
            private Expression<Func<Ad, bool>> GetFilterPredicate(FilterAdsQuery request)
            {
                ExpressionStarter<Ad> predicate = PredicateBuilder.New<Ad>(true);

                if (request.AdType != null)
                {
                    predicate.And(ad => ad.AdType == request.AdType);
                }

                if (request.CategoryIds != null)
                {
                    predicate.And
                    (
                        ad => ad.Categories
                                  .Count(category => request.CategoryIds.Contains(category.CategoryId))
                              == request.CategoryIds.Count()
                    );
                }

                if (request.TagIds != null)
                {
                    predicate.And
                    (
                        ad => ad.Tags
                                  .Count(tag => request.TagIds.Contains(tag.TagId))
                              == request.TagIds.Count()
                    );
                }

                return predicate;
            }

            #endregion
        }

        #endregion
    }
}
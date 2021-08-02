using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Ads.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Primary.Entities;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ads.Queries.AdStorage
{
    public class FilterAdsQuery : IRequest<IPagedList<AdDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion
        
        #region Properties

        public AdType? AdType { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> TagIds { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<FilterAdsQuery, IPagedList<AdDto>>
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

            public async Task<IPagedList<AdDto>> Handle(FilterAdsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<Ad, bool>> predicate = GetFilterPredicate(request);

                return await _context.Ads
                    .AsNoTracking()
                    .Where(predicate)
                    .OrderBy(ad => ad.AdId)
                    .ProjectTo<AdDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
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
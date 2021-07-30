using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Categories.Model;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Statistics.Queries
{
    public class GetTop3AdCategoriesQuery : IRequest<List<CategoryDto>>
    {
        #region Classes

        public class Handler : IRequestHandler<GetTop3AdCategoriesQuery, List<CategoryDto>>
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

            #region IRequestHandler<GetTop3AdCategoriesQuery, IEnumerable<CategoryDto>>

            public async Task<List<CategoryDto>> Handle(GetTop3AdCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Categories
                    .Include(category => category.Ads)
                    .Select(category => new
                    {
                        Category = category, 
                        AdsCount = category.Ads.Count()
                    })
                    .OrderByDescending(x => x.AdsCount)
                    .Take(3)
                    .Select(x => x.Category)
                    .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
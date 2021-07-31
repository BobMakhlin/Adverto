using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Tags.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Statistics.Queries
{
    public class GetTop15TagsByViewsQuery : IRequest<List<TagDto>>
    {
        #region Classes

        public class Handler : IRequestHandler<GetTop15TagsByViewsQuery, List<TagDto>>
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

            #region IRequestHandler<GetTop15TagsByViewsQuery, List<TagDto>>

            public async Task<List<TagDto>> Handle(GetTop15TagsByViewsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Tags
                    .AsNoTracking()
                    .OrderByDescending(tag => tag.Ads.SelectMany(ad => ad.AdViews).Count())
                    .Take(15)
                    .ProjectToListAsync<TagDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
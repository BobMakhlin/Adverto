using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Tags.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Tags.Queries
{
    public class GetPagedListOfTagsQuery : IRequest<IPagedList<TagDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfTagsQuery, IPagedList<TagDto>>
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

            #region IRequestHandler<GetPagedListOfTagsQuery, PagedList<TagDto>>

            public async Task<IPagedList<TagDto>> Handle(GetPagedListOfTagsQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Tags
                    .AsNoTracking()
                    .OrderBy(t => t.TagId)
                    .ProjectTo<TagDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
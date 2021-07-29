using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Categories.Model;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Categories.Queries.CategoryStorage
{
    public class GetPagedListOfCategoriesQuery : IRequest<IPagedList<CategoryDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfCategoriesQuery, IPagedList<CategoryDto>>
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

            #region IRequestHandler<GetPagedListOfCategoriesQuery, IPagedList<CategoryDto>>

            public async Task<IPagedList<CategoryDto>> Handle(GetPagedListOfCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Categories
                    .AsNoTracking()
                    .OrderBy(c => c.CategoryId)
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
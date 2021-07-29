using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Categories.Model;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Categories.Queries.CategoryStorage
{
    public class GetListOfCategoriesQuery : IRequest<List<CategoryDto>>
    {
        #region Classes

        public class Handler : IRequestHandler<GetListOfCategoriesQuery, List<CategoryDto>>
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

            #region IRequestHandler<GetListOfCategoriesQuery, List<CategoryDto>>

            public async Task<List<CategoryDto>> Handle(GetListOfCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Categories
                    .AsNoTracking()
                    .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
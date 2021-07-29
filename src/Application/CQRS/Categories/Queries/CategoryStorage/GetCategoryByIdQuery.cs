using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Categories.Model;
using Application.Persistence.Interfaces;
using AutoMapper;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Categories.Queries.CategoryStorage
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        #region Properties

        public Guid CategoryId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
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

            #region IRequestHandler<GetCategoryByIdQuery, CategoryDto>

            public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.Categories
                           .AsNoTracking()
                           .Where(c => c.CategoryId == request.CategoryId)
                           .ProjectToSingleOrDefaultAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException(nameof(Category), request.CategoryId);
            }

            #endregion
        }

        #endregion
    }
}
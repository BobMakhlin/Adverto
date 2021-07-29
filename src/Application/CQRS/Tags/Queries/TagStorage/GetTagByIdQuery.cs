using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Tags.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Tags.Queries.TagStorage
{
    public class GetTagByIdQuery : IRequest<TagDto>
    {
        #region Properties

        public Guid TagId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetTagByIdQuery, TagDto>
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

            #region IRequestHandler<GetTagByIdQuery, TagDto>

            public async Task<TagDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.Tags
                           .AsNoTracking()
                           .Where(t => t.TagId == request.TagId)
                           .ProjectToSingleOrDefaultAsync<TagDto>(_mapper.ConfigurationProvider, cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException(nameof(Tag), request.TagId);
            }

            #endregion
        }

        #endregion
    }
}
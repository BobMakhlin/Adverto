using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Tags.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Tags.Queries
{
    public class GetListOfTagsQuery : IRequest<List<TagDto>>
    {
        #region Classes

        public class Handler : IRequestHandler<GetListOfTagsQuery, List<TagDto>>
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
            
            #region IRequestHandler<GetListOfTagsQuery, List<TagDto>>

            public async Task<List<TagDto>> Handle(GetListOfTagsQuery request, CancellationToken cancellationToken)
            {
                return await _context.Tags
                    .AsNoTracking()
                    .ProjectToListAsync<TagDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
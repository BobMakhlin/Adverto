using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Ads.Models;
using Application.Persistence.Interfaces;
using Application.Queues.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Ads.Queries.AdStorage
{
    public class GetAdFromQueueQuery : IRequest<AdDto>
    {
        #region Classes

        public class Handler : IRequestHandler<GetAdFromQueueQuery, AdDto>
        {
            #region Fields

            private readonly IAdvertoDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAdQueueEnumerator _adQueueEnumerator;

            #endregion

            #region Constructors

            public Handler(IAdvertoDbContext context, IMapper mapper, IAdQueueEnumerator adQueueEnumerator)
            {
                _context = context;
                _mapper = mapper;
                _adQueueEnumerator = adQueueEnumerator;
            }

            #endregion

            #region IRequestHandler<GetAdFromQueueQuery, AdDto>

            public async Task<AdDto> Handle(GetAdFromQueueQuery request, CancellationToken cancellationToken)
            { 
                bool movedSuccessfully = await _adQueueEnumerator.MoveNextAsync()
                    .ConfigureAwait(false);
                if (!movedSuccessfully)
                {
                    throw new InvalidOperationException("Cannot get the id of the current ad from the queue");
                }
                
                Guid currentAdId = _adQueueEnumerator.Current;
                return await _context.Ads
                    .Where(ad => ad.AdId == currentAdId)
                    .ProjectToSingleOrDefaultAsync<AdDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
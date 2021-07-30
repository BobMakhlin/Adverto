using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage
{
    public class UpdateAdCommand : IRequest
    {
        #region Properties

        public Guid AdId { get; set; }
        public AdType AdType { get; set; }
        public double Cost { get; set; }
        public string Content { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<UpdateAdCommand>
        {
            #region Fields

            private readonly IAdvertoDbContext _context;

            #endregion

            #region Constructors

            public Handler(IAdvertoDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<UpdateAdCommand>

            public async Task<Unit> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
            {
                Ad ad = await _context.Ads.FindAsync(request.AdId)
                            .ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Ad), request.AdId);
                
                UpdateAdProperties(ad, request);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
            
            #region Methods

            /// <summary>
            /// Updates <paramref name="ad"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="ad">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="ad"/> parameter</param>
            private void UpdateAdProperties(Ad ad, UpdateAdCommand request)
            {
                ad.AdType = request.AdType;
                ad.Cost = request.Cost;
                ad.Content = request.Content;
            }

            #endregion
        }

        #endregion
    }
}
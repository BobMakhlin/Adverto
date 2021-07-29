using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdStorage
{
    public class DeleteAdCommand : IRequest
    {
        #region Properties

        public Guid AdId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteAdCommand>
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
            
            #region IRequestHandler<DeleteAdCommand>

            public async Task<Unit> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
            {
                Ad ad = await _context.Ads.FindAsync(request.AdId)
                                        .ConfigureAwait(false)
                                    ?? throw new NotFoundException(nameof(Ad), request.AdId);
                
                _context.Ads.Remove(ad);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ads.Commands.AdTag
{
    public class DeleteTagOfAdCommand : IRequest
    {
        #region Properties

        public Guid AdId { get; set; }
        public Guid TagId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteTagOfAdCommand>
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
            
            #region IRequestHandler<DeleteTagOfAdCommand>

            public async Task<Unit> Handle(DeleteTagOfAdCommand request, CancellationToken cancellationToken)
            {
                Ad ad = await _context.Ads
                            .Include(a => a.Tags)
                            .SingleOrDefaultAsync(a => a.AdId == request.AdId, cancellationToken)
                            .ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Ad), request.AdId);

                Tag tag = await _context.Tags.FindAsync(request.TagId)
                              .ConfigureAwait(false)
                          ?? throw new NotFoundException(nameof(Tag), request.TagId);

                ad.Tags.Remove(tag);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
                
                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}
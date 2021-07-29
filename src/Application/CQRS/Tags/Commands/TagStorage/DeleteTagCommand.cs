using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Tags.Commands.TagStorage
{
    public class DeleteTagCommand : IRequest
    {
        #region Properties

        public Guid TagId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteTagCommand>
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

            #region IRequestHandler<DeleteTagCommand>

            public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
            {
                Tag tag = await _context.Tags.FindAsync(request.TagId)
                              .ConfigureAwait(false)
                          ?? throw new NotFoundException(nameof(Tag), request.TagId);

                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}
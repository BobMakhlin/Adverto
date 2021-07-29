using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Tags.Commands.TagStorage
{
    public class UpdateTagCommand : IRequest
    {
        #region Properties

        public Guid TagId { get; set; }
        public string Title { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<UpdateTagCommand>
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

            #region IRequestHandler<UpdateTagCommand>

            public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
            {
                Tag tag = await _context.Tags.FindAsync(request.TagId)
                              .ConfigureAwait(false)
                          ?? throw new NotFoundException(nameof(Tag), request.TagId);

                UpdateTagProperties(tag, request);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Updates <paramref name="tag"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="tag">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="tag"/> parameter</param>
            private void UpdateTagProperties(Tag tag, UpdateTagCommand request)
            {
                tag.Title = request.Title;
            }

            #endregion
        }

        #endregion
    }
}
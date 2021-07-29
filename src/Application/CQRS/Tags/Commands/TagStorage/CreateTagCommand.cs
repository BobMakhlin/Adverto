using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Tags.Commands.TagStorage
{
    public class CreateTagCommand : IRequest<Guid>
    {
        #region Properties

        public string Title { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<CreateTagCommand, Guid>
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

            #region IRequestHandler<CreateTagCommand, Guid>

            public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
            {
                Tag tag = ConvertToTag(request);

                await CreateTagAsync(tag, cancellationToken)
                    .ConfigureAwait(false);

                return tag.TagId;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="Tag"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="Tag"/></returns>
            private Tag ConvertToTag(CreateTagCommand command)
            {
                return new Tag
                {
                    Title = command.Title
                };
            }

            private async Task CreateTagAsync(Tag tag, CancellationToken cancellationToken)
            {
                _context.Tags.Add(tag);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
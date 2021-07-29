using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Ads.Commands.AdCategory
{
    public class AddCategoryToAdCommand : IRequest
    {
        #region Properties

        public Guid AdId { get; set; }
        public Guid CategoryId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<AddCategoryToAdCommand>
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

            #region IRequestHandler<AddCategoryToAdCommand>

            public async Task<Unit> Handle(AddCategoryToAdCommand request, CancellationToken cancellationToken)
            {
                Ad ad = await _context.Ads.FindAsync(request.AdId)
                            .ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Ad), request.AdId);

                Category category = await _context.Categories.FindAsync(request.CategoryId)
                                        .ConfigureAwait(false)
                                    ?? throw new NotFoundException(nameof(Category), request.CategoryId);
                
                ad.Categories.Add(category);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}
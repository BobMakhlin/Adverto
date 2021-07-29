using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Ads.Commands.AdCategory
{
    public class DeleteCategoryOfAdCommand : IRequest
    {
        #region Properties

        public Guid AdId { get; set; }
        public Guid CategoryId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteCategoryOfAdCommand>
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

            #region IRequestHandler<DeleteCategoryOfAdCommand>

            public async Task<Unit> Handle(DeleteCategoryOfAdCommand request, CancellationToken cancellationToken)
            {
                Ad ad = await _context.Ads
                            .Include(a => a.Categories)
                            .SingleOrDefaultAsync(a => a.AdId == request.AdId, cancellationToken)
                            .ConfigureAwait(false)
                        ?? throw new NotFoundException(nameof(Ad), request.AdId);

                Category category = await _context.Categories
                                        .FindAsync(request.CategoryId)
                                        .ConfigureAwait(false)
                                    ?? throw new NotFoundException(nameof(Category), request.CategoryId);

                ad.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}
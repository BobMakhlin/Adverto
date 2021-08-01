using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EFCore.Extensions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Application.Common.Extensions;


namespace Application.CQRS.Ads.Commands.AdStorage
{
    public class CreateAdCommand : IRequest<Guid>
    {
        #region Properties

        public AdType AdType { get; set; }
        public double Cost { get; set; }
        public string Content { get; set; }
        public Guid[] CategoryIds { get; set; }
        public Guid[] TagIds { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<CreateAdCommand, Guid>
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

            #region IRequestHandler<CreateAdCommand, Guid>

            public async Task<Guid> Handle(CreateAdCommand request, CancellationToken cancellationToken)
            {
                request.CategoryIds ??= new Guid[] { };
                request.TagIds ??= new Guid[] { };

                await _context.Categories.ThrowIfSomeDoNotExistAsync(request.CategoryIds)
                    .ConfigureAwait(false);
                await _context.Tags.ThrowIfSomeDoNotExistAsync(request.TagIds)
                    .ConfigureAwait(false);

                Ad ad = ConvertToAd(request);
                ad.Categories = await _context.Categories.FindManyAsync(request.CategoryIds)
                    .ConfigureAwait(false);
                ad.Tags = await _context.Tags.FindManyAsync(request.TagIds)
                    .ConfigureAwait(false);

                await CreateAdAsync(ad, cancellationToken)
                    .ConfigureAwait(false);

                return ad.AdId;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="Ad"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="Category"/></returns>
            private Ad ConvertToAd(CreateAdCommand command)
            {
                return new Ad
                {
                    AdType = command.AdType,
                    Cost = command.Cost,
                    Content = command.Content
                };
            }

            private async Task CreateAdAsync(Ad ad, CancellationToken cancellationToken)
            {
                _context.Ads.Add(ad);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}
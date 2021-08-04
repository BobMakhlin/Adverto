using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.EFCore.Extensions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.Ads.Commands.AdStorage.Create.Abstractions
{
    /// <summary>
    /// The base class for all ad-creation command handlers.
    /// </summary>
    public abstract class CreateAdCommandHandlerBase
    {
        #region Fields

        protected readonly IAdvertoDbContext _context;

        #endregion

        #region Constructors

        protected CreateAdCommandHandlerBase(IAdvertoDbContext context)
        {
            _context = context;
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Creates an <see cref="Ad"/>, based on properties of the parameter <paramref name="command"/>.
        /// It sends the changes to the database, so there is no need to call
        /// <see cref="IAdvertoDbContext.SaveChangesAsync(CancellationToken)"/>.
        /// </summary>
        /// <returns>
        /// The created ad.
        /// </returns>
        protected async Task<Ad> CreateAdAsync(CreateAdCommandBase command, CancellationToken cancellationToken)
        {
            command.CategoryIds ??= new Guid[] { };
            command.TagIds ??= new Guid[] { };

            await _context.Categories.ThrowIfSomeDoNotExistAsync(command.CategoryIds)
                .ConfigureAwait(false);
            await _context.Tags.ThrowIfSomeDoNotExistAsync(command.TagIds)
                .ConfigureAwait(false);

            var ad = new Ad
            {
                AdType = command.AdType,
                Content = command.Content,
                Cost = command.Cost,
                Categories = await _context.Categories.FindManyAsync(command.CategoryIds)
                    .ConfigureAwait(false),
                Tags = await _context.Tags.FindManyAsync(command.TagIds)
                    .ConfigureAwait(false)
            };

            _context.Ads.Add(ad);

            await _context.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);

            return ad;
        }

        #endregion
    }
}
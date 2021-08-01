using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="DbSet{TEntity}"/>.
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// Throws an exception of type <see cref="NotFoundException"/> if the item
        /// with the specified <paramref name="primaryKeyValues"/> doesn't exist.
        /// </summary>
        public static async Task ThrowIfDoesNotExistAsync<TEntity>(this DbSet<TEntity> dbSet,
            params object[] primaryKeyValues)
            where TEntity : class
        {
            TEntity entity = await dbSet.FindAsync(primaryKeyValues)
                .ConfigureAwait(false);
            
            if (entity == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, primaryKeyValues);
            }
        }

        /// <summary>
        /// Throws an exception of type <see cref="ManyNotFoundException"/>, containing
        /// the info about what entities were not found.
        /// </summary>
        public static async Task ThrowIfSomeDoNotExistAsync<TEntity, TPrimaryKey>(this DbSet<TEntity> dbSet,
            TPrimaryKey[] keys)
            where TEntity : class
        {
            var notFoundExceptions = new List<NotFoundException>();

            foreach (TPrimaryKey key in keys)
            {
                try
                {
                    await dbSet.ThrowIfDoesNotExistAsync(key)
                        .ConfigureAwait(false);
                }
                catch (NotFoundException e)
                {
                    notFoundExceptions.Add(e);
                }
            }

            if (notFoundExceptions.Count > 0)
            {
                throw new ManyNotFoundException(notFoundExceptions);
            }
        }
    }
}
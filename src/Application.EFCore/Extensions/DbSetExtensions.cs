using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.EFCore.Extensions
{
    /// <summary>
    /// Contains extension methods for type <see cref="DbSet{TEntity}"/>
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// Finds an entity for each key in <paramref name="keys"/>-collection.
        /// </summary>
        public static async Task<List<TEntity>> FindManyAsync<TEntity, TPrimaryKey>(this DbSet<TEntity> dbSet,
            TPrimaryKey[] keys) where TEntity : class
        {
            var entities = new List<TEntity>();
            
            foreach (var key in keys)
            {
                TEntity entity = await dbSet.FindAsync(key)
                    .ConfigureAwait(false);
                
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }

            return entities;
        }
    }
}
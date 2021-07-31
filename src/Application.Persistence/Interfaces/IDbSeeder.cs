using System.Threading;
using System.Threading.Tasks;

namespace Application.Persistence.Interfaces
{
    public interface IDbSeeder<TDbContext>
    {
        /// <summary>
        /// Populates the database with data.
        /// </summary>
        /// <returns></returns>
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}
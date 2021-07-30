using System.Threading;
using System.Threading.Tasks;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence.Interfaces
{
    public interface IAdvertoDbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdView> AdViews { get; set; }
        public DbSet<DisabledAd> DisabledAds { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
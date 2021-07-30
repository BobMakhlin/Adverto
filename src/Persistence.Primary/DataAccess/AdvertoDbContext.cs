using System.Reflection;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Primary.DataAccess
{
    public class AdvertoDbContext : DbContext, IAdvertoDbContext
    {
        #region Constructors

        public AdvertoDbContext(DbContextOptions<AdvertoDbContext> options) : base(options)
        {
        }

        #endregion

        #region IAdvertoDbContext

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AdView> AdViews { get; set; }
        public DbSet<DisabledAd> DisabledAds { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}
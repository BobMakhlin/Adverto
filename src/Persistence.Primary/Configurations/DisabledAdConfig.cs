using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class DisabledAdConfig : IEntityTypeConfiguration<DisabledAd>
    {
        public void Configure(EntityTypeBuilder<DisabledAd> builder)
        {
            builder
                .HasKey(da => da.AdId);
            
            builder
                .Property(da => da.DisabledAt)
                .IsRequired();
        }
    }
}
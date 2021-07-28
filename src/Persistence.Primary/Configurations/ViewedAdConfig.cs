using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class ViewedAdConfig : IEntityTypeConfiguration<ViewedAd>
    {
        public void Configure(EntityTypeBuilder<ViewedAd> builder)
        {
            builder
                .HasKey(va => va.AdId);
            
            builder
                .Property(va => va.ViewedAt)
                .IsRequired();
        }
    }
}
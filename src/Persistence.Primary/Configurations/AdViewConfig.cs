using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class AdViewConfig : IEntityTypeConfiguration<AdView>
    {
        public void Configure(EntityTypeBuilder<AdView> builder)
        {
            builder
                .Property(va => va.ViewedAt)
                .IsRequired();
        }
    }
}
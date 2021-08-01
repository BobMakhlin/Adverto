using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class AdConfig : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder
                .Property(a => a.AdId)
                .ValueGeneratedOnAdd();

            builder
                .Property(a => a.AdType)
                .HasConversion<string>()
                .HasMaxLength(64)
                .IsRequired();

            builder
                .Property(a => a.Cost)
                .IsRequired();

            builder
                .Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(3600);

            builder
                .HasIndex(a => a.AdType);
        }
    }
}
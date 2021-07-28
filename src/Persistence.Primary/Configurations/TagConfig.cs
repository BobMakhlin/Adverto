using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .Property(t => t.TagId)
                .ValueGeneratedOnAdd();

            builder
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(64);

            builder
                .HasIndex(t => t.Title)
                .IsUnique();
        }
    }
}
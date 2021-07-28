using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(c => c.CategoryId)
                .ValueGeneratedOnAdd();

            builder
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(64);

            builder
                .HasIndex(c => c.Title)
                .IsUnique();
        }
    }
}
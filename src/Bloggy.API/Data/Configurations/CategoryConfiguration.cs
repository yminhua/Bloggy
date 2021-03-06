using Bloggy.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloggy.API.Data.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure (EntityTypeBuilder<Category> builder)
        {
            builder.ToTable ("Categories");

            builder.HasKey (c => c.Id);

            builder.Property (c => c.Id)
                .IsRequired ();

            builder.Property (c => c.Name)
                .IsRequired ()
                .HasMaxLength (100);
        }
    }
}

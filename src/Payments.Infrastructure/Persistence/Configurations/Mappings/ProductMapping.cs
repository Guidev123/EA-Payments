using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Persistence.Configurations.Mappings;

public sealed class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);

        builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(150);
        builder.Property(x => x.Description).IsRequired().HasColumnType("VARCHAR").HasMaxLength(150);
        builder.Property(x => x.Price).IsRequired().HasColumnType("MONEY");

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

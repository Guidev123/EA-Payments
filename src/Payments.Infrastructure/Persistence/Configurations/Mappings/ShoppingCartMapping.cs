using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Persistence.Configurations.Mappings;

public sealed class ShoppingCartMapping : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);

        builder.Property(x => x.TransactionId).IsRequired();
        builder.Property(x => x.Price).IsRequired().HasColumnType("MONEY");
        builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

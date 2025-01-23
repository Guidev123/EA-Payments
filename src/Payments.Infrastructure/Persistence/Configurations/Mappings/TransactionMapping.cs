using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Persistence.Configurations.Mappings;

public sealed class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);

        builder.Property(x => x.PaymentId).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasColumnType("MONEY");
        builder.Property(x => x.ExternalReference)
            .IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(150);

        builder.HasOne(x => x.ShoppingCart).WithOne(x => x.Transaction)
            .HasForeignKey<ShoppingCart>(x => x.TransactionId);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

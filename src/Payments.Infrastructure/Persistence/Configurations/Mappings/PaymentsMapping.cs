using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;

namespace Payments.Infrastructure.Persistence.Configurations.Mappings;

public sealed class PaymentsMapping : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);

        builder.Property(x => x.CustomerId).IsRequired();
        builder.OwnsOne(x => x.Email, y =>
        {
            y.Property(x => x.Address).HasColumnType("VARCHAR(160)").HasColumnName("Email").IsRequired();
        });

        builder.Property(x => x.OrderCode).IsRequired().HasColumnType("VARCHAR").HasMaxLength(60);
        builder.Property(x => x.Total).IsRequired().HasColumnType("MONEY");
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Gateway).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasOne(x => x.Transaction).WithOne(x => x.Payment)
            .HasForeignKey<Transaction>(x => x.PaymentId);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}

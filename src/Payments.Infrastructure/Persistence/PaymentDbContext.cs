using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using Payments.Infrastructure.Persistence.Configurations.Interceptors;

namespace Payments.Infrastructure.Persistence;

public sealed class PaymentDbContext(DbContextOptions<PaymentDbContext> options)
                  : DbContext(options)
{
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer()
            .AddInterceptors(new SoftDeleteInterceptor());
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

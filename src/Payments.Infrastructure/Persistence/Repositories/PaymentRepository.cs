using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using Payments.Domain.Repositories;

namespace Payments.Infrastructure.Persistence.Repositories;

public sealed class PaymentRepository(PaymentDbContext context) : IPaymentRepository
{
    private readonly PaymentDbContext _context = context;

    public async Task CreateAsync(Payment payment)
        => await _context.Payments.AddAsync(payment);

    public async Task CreateTransactionAsync(Transaction transaction)
        => await _context.AddAsync(transaction);

    public void DeleteAsync(Payment payment)
        => _context.Payments.Remove(payment);

    public async Task<List<Payment>> GetAllAsync(int pageNumber, int pageSize)
        => await _context.Payments.AsNoTracking().AsSplitQuery().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

    public async Task<Payment?> GetByIdAsync() 
        => await _context.Payments.AsNoTracking().AsSplitQuery().FirstOrDefaultAsync();

    public void Update(Payment payment)
        => _context.Payments.Update(payment);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

using Payments.Domain.Entities;

namespace Payments.Domain.Repositories;

public interface IPaymentRepository : IDisposable
{
    Task<List<Payment>> GetAllAsync(int pageNumber, int pageSize);
    Task<Payment?> GetByIdAsync();
    Task<Payment?> GetByOrderCodeAsync(string orderCode);
    Task CreateAsync(Payment payment);
    Task CreateTransactionAsync(Transaction transaction);
    void DeleteAsync(Payment payment);
    void Update(Payment payment);
}

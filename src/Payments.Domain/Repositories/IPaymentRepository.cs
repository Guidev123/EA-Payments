using Payments.Domain.Entities;

namespace Payments.Domain.Repositories;

public interface IPaymentRepository
{
    Task<List<Payment>> GetAllAsync(int pageNumber, int pageSize);
    Task<Payment?> GetByIdAsync();
    Task CreateAsync(Payment payment);
    Task CreateTransactionAsync(Transaction transaction);
    Task DeleteAsync(Payment payment);
    void Update(Payment payment);
}

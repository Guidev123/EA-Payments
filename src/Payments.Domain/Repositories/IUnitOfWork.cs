namespace Payments.Domain.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task<bool> CommitAsync();
    bool HasActiveTransaction();
    Task RollbackTransactionAsync();
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Payments.Domain.Repositories;

namespace Payments.Infrastructure.Persistence.Repositories;

public sealed class UnitOfWork(PaymentDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly PaymentDbContext _context = context;
    public async Task BeginTransactionAsync() =>
        _transaction = await _context.Database.BeginTransactionAsync();

    public async Task<bool> CommitAsync()
    {
        if (_transaction is null) return false;

        try
        {
            await _transaction!.CommitAsync();
            return true;
        }
        catch
        {
            await _transaction!.RollbackAsync();
            return false;
        }
    }

    public async Task<int> CompleteAsync()
    {
        const int maxRetryCount = 3;
        for (int attempt = 1; attempt <= maxRetryCount; attempt++)
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (attempt == maxRetryCount) throw;

                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is not null)
                    {
                        await entry.ReloadAsync();
                    }
                }
            }
        }
        return 0;
    }

    public bool HasActiveTransaction() => _transaction is not null;

    public async Task RollbackTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}

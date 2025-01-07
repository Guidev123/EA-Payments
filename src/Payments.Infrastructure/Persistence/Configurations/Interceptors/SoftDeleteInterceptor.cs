using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Payments.Domain.DomainObjects;

namespace Payments.Infrastructure.Persistence.Configurations.Interceptors;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: Entity delete }) continue;
            entry.State = EntityState.Modified;
            delete.SetAsDeleted();
        }
        return result;
    }
}

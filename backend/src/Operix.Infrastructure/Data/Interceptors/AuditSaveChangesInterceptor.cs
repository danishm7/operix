using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Operix.Application.Interfaces;
using Operix.Domain.Common;

namespace Operix.Infrastructure.Data.Interceptors;

public sealed class AuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditSaveChangesInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var now = DateTimeOffset.UtcNow;
        var userId = _currentUserService.UserId;

        foreach (var entry in eventData.Context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedOn).CurrentValue = now;
                entry.Property(x => x.CreatedBy).CurrentValue = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(x => x.ModifiedOn).CurrentValue = now;
                entry.Property(x => x.ModifiedBy).CurrentValue = userId;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
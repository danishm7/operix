using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class PermissionRepository : IPermissionRepository
{
    private readonly OperixDbContext _dbContext;

    public PermissionRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Permissions
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Permission>> GetByCodesAsync(IReadOnlyList<string> codes, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Permissions
            .Where(x => codes.Contains(x.Code) && x.IsActive)
            .ToListAsync(cancellationToken);
    }
}
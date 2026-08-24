using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class RolePermissionRepository : IRolePermissionRepository
{
    private readonly OperixDbContext _dbContext;

    public RolePermissionRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role?> GetRoleAsync(int roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
    }

    public async Task<IReadOnlyList<Permission>> GetPermissionsAsync(IReadOnlyList<int> permissionIds, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Permissions
            .Where(x => permissionIds.Contains(x.Id) && x.IsActive)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RolePermission>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RolePermissions
            .Where(x => x.RoleId == roleId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(RolePermission rolePermission, CancellationToken cancellationToken = default)
    {
        await _dbContext.RolePermissions.AddAsync(
            rolePermission,
            cancellationToken);
    }

    public Task DeleteAsync(RolePermission rolePermission, CancellationToken cancellationToken = default)
    {
        _dbContext.RolePermissions.Remove(rolePermission);

        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.RolePermissions
            .Where(x => x.RoleId == roleId)
            .Select(x => x.Permission)
            .Where(x => x.IsActive)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
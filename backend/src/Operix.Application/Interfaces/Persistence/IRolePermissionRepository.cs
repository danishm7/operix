using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IRolePermissionRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<Role?> GetRoleAsync(int roleId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permission>> GetPermissionsAsync(IReadOnlyList<int> permissionIds, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RolePermission>> GetByRoleIdAsync(int roleId, CancellationToken cancellationToken = default);
    Task AddAsync(RolePermission rolePermission, CancellationToken cancellationToken = default);
    Task DeleteAsync(RolePermission rolePermission, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permission>> GetPermissionsByRoleIdAsync(int roleId, CancellationToken cancellationToken = default);
}
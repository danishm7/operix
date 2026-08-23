using Operix.Application.DTOs;
using Operix.Application.Exceptions;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Services;

public sealed class RolePermissionService
{
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IApplicationDbContext _dbContext;

    public RolePermissionService(IRolePermissionRepository rolePermissionRepository, IPermissionRepository permissionRepository, IApplicationDbContext dbContext)
    {
        _rolePermissionRepository = rolePermissionRepository;
        _permissionRepository = permissionRepository;
        _dbContext = dbContext;
    }

    public async Task AssignPermissionsAsync(int roleId, AssignPermissionsDto dto, CancellationToken cancellationToken = default)
    {
        var role = await _rolePermissionRepository.GetRoleAsync(roleId, cancellationToken);

        if (role is null)
        {
            throw new NotFoundException($"Role with ID '{roleId}' does not exist.");
        }

        var permissionIds = dto.PermissionIds
            .Distinct()
            .ToList();

        var permissions = await _rolePermissionRepository.GetPermissionsAsync(permissionIds, cancellationToken);

        if (permissions.Count != permissionIds.Count)
        {
            throw new NotFoundException("One or more permissions do not exist or are inactive.");
        }

        var existingRolePermissions = await _rolePermissionRepository.GetByRoleIdAsync(roleId, cancellationToken);

        foreach (var rolePermission in existingRolePermissions)
        {
            await _rolePermissionRepository.DeleteAsync(rolePermission, cancellationToken);
        }

        foreach (var permission in permissions)
        {
            var rolePermission = new RolePermission(roleId, permission.Id);

            await _rolePermissionRepository.AddAsync(rolePermission, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PermissionDto>> GetPermissionsAsync(int roleId, CancellationToken cancellationToken = default)
    {
        var role = await _rolePermissionRepository.GetRoleAsync(roleId, cancellationToken);

        if (role is null)
        {
            throw new NotFoundException($"Role with ID '{roleId}' does not exist.");
        }

        var permissions = await _rolePermissionRepository.GetPermissionsByRoleIdAsync(roleId, cancellationToken);

        return [.. permissions.Select(x => new PermissionDto(
            x.Id,
            x.Name,
            x.Code,
            x.Description,
            x.IsActive))];
    }

    public async Task<IReadOnlyList<PermissionDto>> GetAllPermissionsAsync(CancellationToken cancellationToken = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(cancellationToken);

        return [.. permissions.Select(x => new PermissionDto(
            x.Id,
            x.Name,
            x.Code,
            x.Description,
            x.IsActive))];
    }
}
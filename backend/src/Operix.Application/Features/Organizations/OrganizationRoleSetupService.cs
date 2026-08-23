using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Features.Organizations;

public sealed class OrganizationRoleSetupService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public OrganizationRoleSetupService(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task SetupAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        var roles = DefaultRolePermissions.Roles
            .Select(x => new Role(
                organization,
                x.Key,
                $"Default {x.Key} role."))
            .ToList();

        await _roleRepository.AddRangeAsync(roles, cancellationToken);

        var permissionCodes = DefaultRolePermissions.Roles
            .SelectMany(x => x.Value)
            .Distinct()
            .ToList();

        var permissions = await _permissionRepository.GetByCodesAsync(permissionCodes, cancellationToken);

        if (permissions.Count != permissionCodes.Count)
        {
            throw new InvalidOperationException("One or more default role permissions could not be found.");
        }

        foreach (var role in roles)
        {
            var rolePermissionCodes = DefaultRolePermissions.Roles[role.Name];

            var rolePermissions = permissions
                .Where(x => rolePermissionCodes.Contains(x.Code))
                .Select(x => new RolePermission(role, x))
                .ToList();

            foreach (var rolePermission in rolePermissions)
            {
                await _rolePermissionRepository.AddAsync(rolePermission, cancellationToken);
            }
        }
    }
}
namespace Operix.Application.Features.RolePermissions;

public sealed record AssignPermissionsDto(
    IReadOnlyList<int> PermissionIds);
namespace Operix.Application.DTOs;

public sealed record AssignPermissionsDto(
    IReadOnlyList<int> PermissionIds);
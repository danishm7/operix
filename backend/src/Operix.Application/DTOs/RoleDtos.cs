namespace Operix.Application.DTOs;

public sealed record RoleDto(
    int Id,
    int? OrganizationId,
    string Name,
    string? Description,
    bool IsActive);

public sealed record CreateRoleDto(
    int? OrganizationId,
    string Name,
    string? Description);

public sealed record UpdateRoleDto(
    string Name,
    string? Description,
    bool IsActive);
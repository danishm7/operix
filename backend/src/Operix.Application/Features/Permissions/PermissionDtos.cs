namespace Operix.Application.Features.Permissions;

public sealed record PermissionDto(
    int Id,
    string Name,
    string Code,
    string? Description,
    bool IsActive);
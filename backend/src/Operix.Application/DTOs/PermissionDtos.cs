namespace Operix.Application.DTOs;

public sealed record PermissionDto(
    int Id,
    string Name,
    string Code,
    string? Description,
    bool IsActive);
namespace Operix.Application.DTOs;

public sealed record OrganizationDto(
    int Id,
    string Name,
    string Code,
    bool IsActive);

public sealed record CreateOrganizationDto(
    string Name,
    string Code);

public sealed record UpdateOrganizationDto(
    string Name,
    string Code,
    bool IsActive);
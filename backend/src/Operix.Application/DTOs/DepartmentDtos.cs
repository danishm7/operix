namespace Operix.Application.DTOs;

public sealed record DepartmentDto(
    int Id,
    int OrganizationId,
    int? ParentDepartmentId,
    string Name,
    string Code,
    bool IsActive);

public sealed record CreateDepartmentDto(
    int OrganizationId,
    int? ParentDepartmentId,
    string Name,
    string Code);

public sealed record UpdateDepartmentDto(
    int? ParentDepartmentId,
    string Name,
    string Code,
    bool IsActive);
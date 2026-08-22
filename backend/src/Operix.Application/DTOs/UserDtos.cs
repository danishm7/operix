namespace Operix.Application.DTOs;

public sealed record UserDto(
    int Id,
    int OrganizationId,
    int? DepartmentId,
    string FirstName,
    string? LastName,
    string Email,
    bool IsActive);

public sealed record CreateUserDto(
    int OrganizationId,
    int? DepartmentId,
    string FirstName,
    string? LastName,
    string Email,
    string Password);

public sealed record UpdateUserDto(
    int? DepartmentId,
    string FirstName,
    string? LastName,
    string Email,
    bool IsActive);
namespace Operix.Application.DTOs.Organization;

public sealed class OrganizationDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
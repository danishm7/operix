namespace Operix.Application.DTOs.Organization;

public sealed class CreateOrganizationDto
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
}
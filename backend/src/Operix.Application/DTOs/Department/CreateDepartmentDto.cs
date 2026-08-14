namespace Operix.Application.DTOs.Department;

public sealed class CreateDepartmentDto
{
    public int OrganizationId { get; set; }
    public int? ParentDepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
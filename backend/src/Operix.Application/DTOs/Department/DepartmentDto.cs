namespace Operix.Application.DTOs.Department;

public sealed class DepartmentDto
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public int? ParentDepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
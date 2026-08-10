using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class Department : AuditableEntity
{
    public int OrganizationId { get; private set; }
    public int? ParentDepartmentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public Department? ParentDepartment { get; private set; }
    public ICollection<Department> ChildDepartments { get; private set; } = [];
    public ICollection<User> Users { get; private set; } = [];
}
using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class User : AuditableEntity
{
    public int OrganizationId { get; private set; }

    public int? DepartmentId { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string? LastName { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    // Navigation Properties

    public Organization Organization { get; private set; } = null!;

    public Department? Department { get; private set; } = null!;

    public ICollection<UserRole> UserRoles { get; private set; } = [];
    public ICollection<WorkOrder> AssignedWorkOrders { get; private set; } = [];
}
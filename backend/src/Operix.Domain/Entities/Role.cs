using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class Role : AuditableEntity
{
    public Role(string name, string? description)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }

    public Role(int? organizationId, string name, string? description)
    {
        OrganizationId = organizationId;
        Name = name;
        Description = description;
        IsActive = true;
    }

    public Role(Organization organization, string name, string? description)
    {
        Organization = organization;
        Name = name;
        Description = description;
        IsActive = true;
    }

    public void Update(string name, string? description, bool isActive)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
    }

    public int? OrganizationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public ICollection<UserRole> UserRoles { get; private set; } = [];
    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
}
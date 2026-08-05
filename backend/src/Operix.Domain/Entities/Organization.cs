using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class Organization : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    // Navigation Properties

    public ICollection<Department> Departments { get; private set; } = [];

    public ICollection<Location> Locations { get; private set; } = [];

    public ICollection<User> Users { get; private set; } = [];

    public ICollection<Role> Roles { get; private set; } = [];
}
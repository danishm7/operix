using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class Location : AuditableEntity
{
    public int OrganizationId { get; private set; }
    public int? ParentLocationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public Location? ParentLocation { get; private set; }
    public ICollection<Location> ChildLocations { get; private set; } = [];
    public ICollection<Asset> Assets { get; private set; } = [];
    public ICollection<Inventory> Inventories { get; private set; } = [];
}
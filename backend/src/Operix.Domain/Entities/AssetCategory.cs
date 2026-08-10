using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class AssetCategory : AuditableEntity
{
    public int OrganizationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public ICollection<Asset> Assets { get; private set; } = [];
}
using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class SparePart : AuditableEntity
{
    public int OrganizationId { get; private set; }

    public int? VendorId { get; private set; }

    public string PartNumber { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string UnitOfMeasure { get; private set; } = string.Empty;

    public decimal MinimumStock { get; private set; }

    public decimal ReorderLevel { get; private set; }

    public decimal? UnitCost { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Properties

    public Organization Organization { get; private set; } = null!;

    public Vendor? Vendor { get; private set; }

    public ICollection<Inventory> Inventories { get; private set; } = [];

    public ICollection<StockTransaction> StockTransactions { get; private set; } = [];
}
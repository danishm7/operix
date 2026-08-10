using Operix.Domain.Common;
using Operix.Domain.Enums;

namespace Operix.Domain.Entities;

public sealed class Asset : AuditableEntity
{
    public int OrganizationId { get; private set; }
    public int AssetCategoryId { get; private set; }
    public int LocationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? SerialNumber { get; private set; }
    public string? Manufacturer { get; private set; }
    public string? Model { get; private set; }
    public DateOnly? InstallationDate { get; private set; }
    public DateOnly? PurchaseDate { get; private set; }
    public DateOnly? WarrantyExpiryDate { get; private set; }
    public AssetStatus Status { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public AssetCategory AssetCategory { get; private set; } = null!;
    public Location Location { get; private set; } = null!;
    public ICollection<AssetDocument> AssetDocuments { get; private set; } = [];
    public ICollection<PreventiveMaintenancePlan> PreventiveMaintenancePlans { get; private set; } = [];
    public ICollection<WorkOrder> WorkOrders { get; private set; } = [];
}
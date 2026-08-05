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

    public ICollection<Asset> Assets { get; private set; } = [];

    public ICollection<AssetCategory> AssetCategories { get; private set; } = [];
    public ICollection<Vendor> Vendors { get; private set; } = [];
    public ICollection<PreventiveMaintenancePlan> PreventiveMaintenancePlans { get; private set; } = [];
    public ICollection<WorkOrder> WorkOrders { get; private set; } = [];
    public ICollection<SparePart> SpareParts { get; private set; } = [];
    public ICollection<Inventory> Inventories { get; private set; } = [];
    public ICollection<StockTransaction> StockTransactions { get; private set; } = [];
}
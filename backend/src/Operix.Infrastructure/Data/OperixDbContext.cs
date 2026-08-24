using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data;

public class OperixDbContext : DbContext, IApplicationDbContext
{
    public OperixDbContext(DbContextOptions<OperixDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OperixDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    // core entities
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();


    // cmms entities
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<AssetCategory> AssetCategories => Set<AssetCategory>();
    public DbSet<AssetDocument> AssetDocuments => Set<AssetDocument>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<PreventiveMaintenancePlan> PreventiveMaintenancePlans => Set<PreventiveMaintenancePlan>();
    public DbSet<MaintenanceSchedule> MaintenanceSchedules => Set<MaintenanceSchedule>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<WorkOrderTask> WorkOrderTasks => Set<WorkOrderTask>();
    public DbSet<SparePart> SpareParts => Set<SparePart>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();
}
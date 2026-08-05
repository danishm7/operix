using Microsoft.EntityFrameworkCore;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data;

public class OperixDbContext : DbContext
{
    public OperixDbContext(DbContextOptions<OperixDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OperixDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
}
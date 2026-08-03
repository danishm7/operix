using Microsoft.EntityFrameworkCore;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data;

public class OperixDbContext : DbContext
{
    public OperixDbContext(DbContextOptions<OperixDbContext> options): base(options)
    {
    }

    public DbSet<Organization> Organizations => Set<Organization>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cmms");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OperixDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
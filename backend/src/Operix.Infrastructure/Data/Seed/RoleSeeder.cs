using Microsoft.EntityFrameworkCore;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Data.Seed;

public static class RoleSeeder
{
    public static async Task SeedAsync(OperixDbContext dbContext)
    {
        var role = await dbContext.Roles
            .FirstOrDefaultAsync(x =>
                x.OrganizationId == null &&
                x.Name == "System Administrator");

        if (role is null)
        {
            role = new Role(
                "System Administrator",
                "Full access to Operix.");

            await dbContext.Roles.AddAsync(role);
            await dbContext.SaveChangesAsync();
        }

        var permissions = await dbContext.Permissions
            .Where(x => x.IsActive)
            .ToListAsync();

        var existingPermissionIds = await dbContext.RolePermissions
            .Where(x => x.RoleId == role.Id)
            .Select(x => x.PermissionId)
            .ToHashSetAsync();

        var rolePermissions = permissions
            .Where(x => !existingPermissionIds.Contains(x.Id))
            .Select(x => new RolePermission(role.Id, x.Id))
            .ToList();

        if (rolePermissions.Count == 0)
        {
            return;
        }

        await dbContext.RolePermissions.AddRangeAsync(rolePermissions);
        await dbContext.SaveChangesAsync();
    }
}
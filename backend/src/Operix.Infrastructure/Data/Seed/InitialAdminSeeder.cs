using Microsoft.EntityFrameworkCore;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data.Seed;

public static class InitialAdminSeeder
{
    private const string AdminEmail = "danish@example.com";
    private const string AdminRoleName = "Organization Administrator";

    public static async Task SeedAsync(OperixDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == AdminEmail, cancellationToken);

        if (user is null)
        {
            return;
        }

        var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.OrganizationId == user.OrganizationId && x.Name == AdminRoleName, cancellationToken);

        if (role is null)
        {
            throw new InvalidOperationException($"The '{AdminRoleName}' role was not found for organization {user.OrganizationId}.");
        }

        var userRoleExists = await dbContext.UserRoles.AnyAsync(x => x.UserId == user.Id && x.RoleId == role.Id, cancellationToken);

        if (userRoleExists)
        {
            return;
        }

        var userRole = new UserRole(user.Id, role.Id);

        dbContext.UserRoles.Add(userRole);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
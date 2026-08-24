using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly OperixDbContext _dbContext;

    public UserRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> OrganizationExistsAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations.AnyAsync(x => x.Id == organizationId, cancellationToken);
    }

    public async Task<bool> DepartmentExistsAsync(int departmentId, int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.AnyAsync(x => x.Id == departmentId && x.OrganizationId == organizationId, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AsNoTracking().Where(x => x.OrganizationId == organizationId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public Task DeleteAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Users.Remove(user);

        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<string>> GetPermissionCodesAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .SelectMany(x => x.Role.RolePermissions)
            .Where(x => x.Permission.IsActive)
            .Select(x => x.Permission.Code)
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
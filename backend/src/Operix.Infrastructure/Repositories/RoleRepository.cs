using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class RoleRepository : IRoleRepository
{
    private readonly OperixDbContext _dbContext;

    public RoleRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByNameAsync(int? organizationId, string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AnyAsync(
                x => x.OrganizationId == organizationId && x.Name == name,
                cancellationToken);
    }

    public async Task<bool> OrganizationExistsAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations
            .AnyAsync(x => x.Id == organizationId, cancellationToken);
    }

    public async Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Role?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddAsync(role, cancellationToken);
    }

    public Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        _dbContext.Roles.Remove(role);

        return Task.CompletedTask;
    }

    public async Task AddRangeAsync(IReadOnlyList<Role> roles, CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddRangeAsync(roles, cancellationToken);
    }
}
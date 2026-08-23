using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class OrganizationRepository : IOrganizationRepository
{
    private readonly OperixDbContext _dbContext;

    public OrganizationRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations.AnyAsync(x => x.Code == code, cancellationToken);
    }

    public async Task AddAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        await _dbContext.Organizations.AddAsync(organization, cancellationToken);
    }

    public async Task<Organization?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Organization?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task DeleteAsync(Organization organization, CancellationToken cancellationToken = default)
    {
        _dbContext.Organizations.Remove(organization);

        return Task.CompletedTask;
    }
}
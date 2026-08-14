using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class DepartmentRepository : IDepartmentRepository
{
    private readonly OperixDbContext _dbContext;

    public DepartmentRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(int organizationId, string code, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.AnyAsync(x => x.OrganizationId == organizationId && x.Code == code, cancellationToken);
    }

    public async Task<bool> OrganizationExistsAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Organizations.AnyAsync(x => x.Id == organizationId, cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Department?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Department>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments.AsNoTracking().Where(x => x.OrganizationId == organizationId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Department department, CancellationToken cancellationToken = default)
    {
        await _dbContext.Departments.AddAsync(department, cancellationToken);
    }

    public Task DeleteAsync(Department department, CancellationToken cancellationToken = default)
    {
        _dbContext.Departments.Remove(department);

        return Task.CompletedTask;
    }
}
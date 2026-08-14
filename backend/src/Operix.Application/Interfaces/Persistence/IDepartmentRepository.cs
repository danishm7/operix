using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IDepartmentRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(int organizationId, string code, CancellationToken cancellationToken = default);
    Task<bool> OrganizationExistsAsync(int organizationId, CancellationToken cancellationToken = default);
    Task<Department?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Department?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Department>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default);
    Task AddAsync(Department department, CancellationToken cancellationToken = default);
    Task DeleteAsync(Department department, CancellationToken cancellationToken = default);
}
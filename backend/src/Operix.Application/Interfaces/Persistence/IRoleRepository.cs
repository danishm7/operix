using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IRoleRepository
{
    Task<bool> ExistsByNameAsync(int? organizationId, string name, CancellationToken cancellationToken = default);
    Task<bool> OrganizationExistsAsync(int organizationId, CancellationToken cancellationToken = default);
    Task<Role?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Role?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default);
    Task AddAsync(Role role, CancellationToken cancellationToken = default);
    Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IReadOnlyList<Role> roles, CancellationToken cancellationToken = default);
}
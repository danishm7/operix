using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IOrganizationRepository
{
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task AddAsync(Organization organization, CancellationToken cancellationToken = default);
    Task<Organization?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Organization>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Organization?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Organization organization, CancellationToken cancellationToken = default);
}
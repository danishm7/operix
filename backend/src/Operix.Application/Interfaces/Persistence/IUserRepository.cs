using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> OrganizationExistsAsync(int organizationId, CancellationToken cancellationToken = default);
    Task<bool> DepartmentExistsAsync(int departmentId, int organizationId, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<User?> GetTrackedByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(User user, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetPermissionCodesAsync(int userId, CancellationToken cancellationToken = default);
}
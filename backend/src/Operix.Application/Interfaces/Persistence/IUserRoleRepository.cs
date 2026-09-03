using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IUserRoleRepository
{
    Task<IReadOnlyList<UserRole>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default);
    Task DeleteAsync(UserRole userRole, CancellationToken cancellationToken = default);
}
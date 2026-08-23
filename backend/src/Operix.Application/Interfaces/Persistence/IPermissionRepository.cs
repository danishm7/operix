using Operix.Domain.Entities;

namespace Operix.Application.Interfaces.Persistence;

public interface IPermissionRepository
{
    Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permission>> GetByCodesAsync(IReadOnlyList<string> codes, CancellationToken cancellationToken = default);
}
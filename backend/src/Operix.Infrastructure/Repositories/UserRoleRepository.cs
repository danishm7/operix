using Microsoft.EntityFrameworkCore;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Operix.Infrastructure.Data;

namespace Operix.Infrastructure.Repositories;

public sealed class UserRoleRepository : IUserRoleRepository
{
    private readonly OperixDbContext _dbContext;

    public UserRoleRepository(OperixDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<UserRole>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Include(x => x.Role).Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserRoles.AddAsync(userRole, cancellationToken);
    }

    public Task DeleteAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        _dbContext.UserRoles.Remove(userRole);

        return Task.CompletedTask;
    }
}
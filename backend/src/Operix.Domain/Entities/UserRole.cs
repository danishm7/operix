using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class UserRole : AuditableEntity
{
    public int UserId { get; private set; }
    public int RoleId { get; private set; }

    // Navigation Properties
    public User User { get; private set; } = null!;
    public Role Role { get; private set; } = null!;
}
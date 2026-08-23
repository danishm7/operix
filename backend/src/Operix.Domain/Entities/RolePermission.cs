using Operix.Domain.Common;
namespace Operix.Domain.Entities;

public sealed class RolePermission : AuditableEntity
{
    private int id;

    public RolePermission(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }

    // Navigation Properties
    public Role Role { get; private set; } = null!;
    public Permission Permission { get; private set; } = null!;
}
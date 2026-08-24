using Operix.Domain.Common;
namespace Operix.Domain.Entities;

public sealed class Permission : AuditableEntity
{
    public Permission(string name, string code)
    {
        Name = name;
        Code = code;
        IsActive = true;
    }

    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation Properties
    public ICollection<RolePermission> RolePermissions { get; private set; } = [];
}
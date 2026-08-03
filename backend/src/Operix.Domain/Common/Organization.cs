using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public class Organization : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
}
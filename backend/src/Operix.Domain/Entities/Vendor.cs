using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class Vendor : AuditableEntity
{
    public int OrganizationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? ContactPerson { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Website { get; private set; }
    public string? Address { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public ICollection<SparePart> SpareParts { get; private set; } = [];
}
namespace Operix.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTimeOffset CreatedOn { get; private set; }

    public int? CreatedBy { get; private set; }

    public DateTimeOffset? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }
}
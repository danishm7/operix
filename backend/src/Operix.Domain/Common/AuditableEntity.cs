namespace Operix.Domain.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedOn { get; private set; }

    public int? CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }
}
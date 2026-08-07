using Operix.Domain.Common;
using Operix.Domain.Enums;

namespace Operix.Domain.Entities;

public sealed class WorkOrderTask : AuditableEntity
{
    public int WorkOrderId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public int Sequence { get; private set; }

    public WorkOrderStatus Status { get; private set; }

    public int? EstimatedDuration { get; private set; }

    public int? ActualDuration { get; private set; }

    public DateTimeOffset? CompletedDate { get; private set; }

    // Navigation Properties

    public WorkOrder WorkOrder { get; private set; } = null!;
}
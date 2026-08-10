using Operix.Domain.Common;
using Operix.Domain.Enums;

namespace Operix.Domain.Entities;

public sealed class MaintenanceSchedule : AuditableEntity
{
    public int PreventiveMaintenancePlanId { get; private set; }
    public DateOnly ScheduledDate { get; private set; }
    public DateTimeOffset? CompletedDate { get; private set; }
    public MaintenanceScheduleStatus Status { get; private set; }
    public int? WorkOrderId { get; private set; }
    public string? Remarks { get; private set; }

    // Navigation Properties
    public PreventiveMaintenancePlan PreventiveMaintenancePlan { get; private set; } = null!;
    public WorkOrder? WorkOrder { get; private set; }
}
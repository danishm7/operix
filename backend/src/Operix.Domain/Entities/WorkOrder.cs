using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class WorkOrder : AuditableEntity
{
    public int OrganizationId { get; private set; }

    public int AssetId { get; private set; }

    public int? PreventiveMaintenancePlanId { get; private set; }

    public int? AssignedUserId { get; private set; }

    public string WorkOrderNumber { get; private set; } = string.Empty;

    public string Title { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string Priority { get; private set; } = string.Empty;

    public string Status { get; private set; } = string.Empty;

    public DateOnly? ScheduledDate { get; private set; }

    public DateOnly? DueDate { get; private set; }

    public DateOnly? CompletedDate { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Properties

    public Organization Organization { get; private set; } = null!;

    public Asset Asset { get; private set; } = null!;

    public PreventiveMaintenancePlan? PreventiveMaintenancePlan { get; private set; }

    public User? AssignedUser { get; private set; }

    public MaintenanceSchedule? MaintenanceSchedule { get; private set; }

    public ICollection<WorkOrderTask> WorkOrderTasks { get; private set; } = [];
    public ICollection<StockTransaction> StockTransactions { get; private set; } = [];
}
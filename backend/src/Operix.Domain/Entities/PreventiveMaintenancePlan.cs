using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class PreventiveMaintenancePlan : AuditableEntity
{
    public int OrganizationId { get; private set; }

    public int AssetId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string Code { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string Frequency { get; private set; } = string.Empty;

    public DateOnly StartDate { get; private set; }

    public DateOnly? EndDate { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation Properties

    public Organization Organization { get; private set; } = null!;

    public Asset Asset { get; private set; } = null!;

    public ICollection<MaintenanceSchedule> MaintenanceSchedules { get; private set; } = [];

    public ICollection<WorkOrder> WorkOrders { get; private set; } = [];
}
using Operix.Domain.Common;
using Operix.Domain.Enums;

namespace Operix.Domain.Entities;

public sealed class StockTransaction : AuditableEntity
{
    public int OrganizationId { get; private set; }

    public int InventoryId { get; private set; }

    public int? WorkOrderId { get; private set; }

    public StockTransactionType TransactionType { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal? UnitCost { get; private set; }

    public DateTimeOffset TransactionDate { get; private set; }

    public string? Remarks { get; private set; }

    // Navigation Properties

    public Organization Organization { get; private set; } = null!;

    public Inventory Inventory { get; private set; } = null!;

    public WorkOrder? WorkOrder { get; private set; }
}
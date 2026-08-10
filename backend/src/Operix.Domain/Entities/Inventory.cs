using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class Inventory : AuditableEntity
{
    public int OrganizationId { get; private set; }
    public int LocationId { get; private set; }
    public int SparePartId { get; private set; }
    public decimal QuantityOnHand { get; private set; }
    public decimal QuantityReserved { get; private set; }
    public decimal QuantityAvailable => QuantityOnHand - QuantityReserved;
    public DateOnly? LastStockedDate { get; private set; }

    // Navigation Properties
    public Organization Organization { get; private set; } = null!;
    public Location Location { get; private set; } = null!;
    public SparePart SparePart { get; private set; } = null!;
    public ICollection<StockTransaction> StockTransactions { get; private set; } = [];
}
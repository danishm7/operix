using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data.Configurations.Cmms;

public sealed class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("inventory", "cmms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityOnHand)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.QuantityReserved)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.LastStockedDate);

        builder.HasIndex(x => new
        {
            x.OrganizationId,
            x.LocationId,
            x.SparePartId
        }).IsUnique();

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.Inventories)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Location)
            .WithMany(x => x.Inventories)
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SparePart)
            .WithMany(x => x.Inventories)
            .HasForeignKey(x => x.SparePartId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
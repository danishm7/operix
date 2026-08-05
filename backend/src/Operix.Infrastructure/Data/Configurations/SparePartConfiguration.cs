using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data.Configurations.Cmms;

public sealed class SparePartConfiguration : IEntityTypeConfiguration<SparePart>
{
    public void Configure(EntityTypeBuilder<SparePart> builder)
    {
        builder.ToTable("spare_part", "cmms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PartNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.UnitOfMeasure)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.MinimumStock)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ReorderLevel)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.UnitCost)
            .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => new { x.OrganizationId, x.PartNumber })
            .IsUnique();

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.SpareParts)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Vendor)
            .WithMany(x => x.SpareParts)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
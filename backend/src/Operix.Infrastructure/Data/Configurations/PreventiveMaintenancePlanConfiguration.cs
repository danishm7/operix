using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data.Configurations.Cmms;

public sealed class PreventiveMaintenancePlanConfiguration : IEntityTypeConfiguration<PreventiveMaintenancePlan>
{
    public void Configure(EntityTypeBuilder<PreventiveMaintenancePlan> builder)
    {
        builder.ToTable("preventive_maintenance_plan", "cmms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.FrequencyType)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.FrequencyInterval)
            .IsRequired();

        builder.Property(x => x.NextDueDate)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => new { x.OrganizationId, x.Code })
            .IsUnique();

        builder.HasOne(x => x.Organization)
            .WithMany(x => x.PreventiveMaintenancePlans)
            .HasForeignKey(x => x.OrganizationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Asset)
            .WithMany(x => x.PreventiveMaintenancePlans)
            .HasForeignKey(x => x.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
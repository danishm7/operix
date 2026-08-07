using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Operix.Domain.Entities;

namespace Operix.Infrastructure.Data.Configurations.Cmms;

public sealed class MaintenanceScheduleConfiguration : IEntityTypeConfiguration<MaintenanceSchedule>
{
    public void Configure(EntityTypeBuilder<MaintenanceSchedule> builder)
    {
        builder.ToTable("maintenance_schedule", "cmms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ScheduledDate)
            .IsRequired();

        builder.Property(x => x.CompletedDate);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Remarks)
            .HasMaxLength(500);

        builder.HasOne(x => x.PreventiveMaintenancePlan)
            .WithMany(x => x.MaintenanceSchedules)
            .HasForeignKey(x => x.PreventiveMaintenancePlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.WorkOrder)
            .WithOne(x => x.MaintenanceSchedule)
            .HasForeignKey<MaintenanceSchedule>(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
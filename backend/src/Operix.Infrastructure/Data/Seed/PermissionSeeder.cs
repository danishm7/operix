using Microsoft.EntityFrameworkCore;
using Operix.Domain.Entities;

using Operix.Application.Authorization;

namespace Operix.Infrastructure.Data.Seed;

public static class PermissionSeeder
{
    public static async Task SeedAsync(OperixDbContext dbContext)
    {
        var existingCodes = await dbContext.Permissions
            .Select(x => x.Code)
            .ToHashSetAsync();

        var permissions = new[]
        {
            new Permission("Organization Read", PermissionCodes.OrganizationRead),
            new Permission("Organization Create", PermissionCodes.OrganizationCreate),
            new Permission("Organization Update", PermissionCodes.OrganizationUpdate),

            new Permission("User Read", PermissionCodes.UserRead),
            new Permission("User Create", PermissionCodes.UserCreate),
            new Permission("User Update", PermissionCodes.UserUpdate),
            new Permission("User Delete", PermissionCodes.UserDelete),

            new Permission("Role Read", PermissionCodes.RoleRead),
            new Permission("Role Create", PermissionCodes.RoleCreate),
            new Permission("Role Update", PermissionCodes.RoleUpdate),
            new Permission("Role Delete", PermissionCodes.RoleDelete),

            new Permission("Permission Read", PermissionCodes.PermissionRead),

            new Permission("Department Read", PermissionCodes.DepartmentRead),
            new Permission("Department Create", PermissionCodes.DepartmentCreate),
            new Permission("Department Update", PermissionCodes.DepartmentUpdate),
            new Permission("Department Delete", PermissionCodes.DepartmentDelete),

            new Permission("Location Read", PermissionCodes.LocationRead),
            new Permission("Location Create", PermissionCodes.LocationCreate),
            new Permission("Location Update", PermissionCodes.LocationUpdate),
            new Permission("Location Delete", PermissionCodes.LocationDelete),

            new Permission("Asset Category Read", PermissionCodes.AssetCategoryRead),
            new Permission("Asset Category Create", PermissionCodes.AssetCategoryCreate),
            new Permission("Asset Category Update", PermissionCodes.AssetCategoryUpdate),
            new Permission("Asset Category Delete", PermissionCodes.AssetCategoryDelete),

            new Permission("Asset Read", PermissionCodes.AssetRead),
            new Permission("Asset Create", PermissionCodes.AssetCreate),
            new Permission("Asset Update", PermissionCodes.AssetUpdate),
            new Permission("Asset Delete", PermissionCodes.AssetDelete),

            new Permission("Vendor Read", PermissionCodes.VendorRead),
            new Permission("Vendor Create", PermissionCodes.VendorCreate),
            new Permission("Vendor Update", PermissionCodes.VendorUpdate),
            new Permission("Vendor Delete", PermissionCodes.VendorDelete),

            new Permission("Preventive Maintenance Read", PermissionCodes.PreventiveMaintenanceRead),
            new Permission("Preventive Maintenance Create", PermissionCodes.PreventiveMaintenanceCreate),
            new Permission("Preventive Maintenance Update", PermissionCodes.PreventiveMaintenanceUpdate),
            new Permission("Preventive Maintenance Delete", PermissionCodes.PreventiveMaintenanceDelete),

            new Permission("Maintenance Schedule Read", PermissionCodes.MaintenanceScheduleRead),
            new Permission("Maintenance Schedule Create", PermissionCodes.MaintenanceScheduleCreate),
            new Permission("Maintenance Schedule Update", PermissionCodes.MaintenanceScheduleUpdate),
            new Permission("Maintenance Schedule Delete", PermissionCodes.MaintenanceScheduleDelete),

            new Permission("Work Order Read", PermissionCodes.WorkOrderRead),
            new Permission("Work Order Create", PermissionCodes.WorkOrderCreate),
            new Permission("Work Order Update", PermissionCodes.WorkOrderUpdate),
            new Permission("Work Order Delete", PermissionCodes.WorkOrderDelete),
            new Permission("Work Order Assign", PermissionCodes.WorkOrderAssign),
            new Permission("Work Order Complete", PermissionCodes.WorkOrderComplete),

            new Permission("Work Order Task Read", PermissionCodes.WorkOrderTaskRead),
            new Permission("Work Order Task Create", PermissionCodes.WorkOrderTaskCreate),
            new Permission("Work Order Task Update", PermissionCodes.WorkOrderTaskUpdate),
            new Permission("Work Order Task Delete", PermissionCodes.WorkOrderTaskDelete),

            new Permission("Spare Part Read", PermissionCodes.SparePartRead),
            new Permission("Spare Part Create", PermissionCodes.SparePartCreate),
            new Permission("Spare Part Update", PermissionCodes.SparePartUpdate),
            new Permission("Spare Part Delete", PermissionCodes.SparePartDelete),

            new Permission("Inventory Read", PermissionCodes.InventoryRead),
            new Permission("Inventory Create", PermissionCodes.InventoryCreate),
            new Permission("Inventory Update", PermissionCodes.InventoryUpdate),
            new Permission("Inventory Delete", PermissionCodes.InventoryDelete),

            new Permission("Stock Transaction Read", PermissionCodes.StockTransactionRead),
            new Permission("Stock Transaction Create", PermissionCodes.StockTransactionCreate)
        };

        var newPermissions = permissions
            .Where(x => !existingCodes.Contains(x.Code))
            .ToList();

        if (newPermissions.Count == 0)
        {
            return;
        }

        await dbContext.Permissions.AddRangeAsync(newPermissions);
        await dbContext.SaveChangesAsync();
    }
}
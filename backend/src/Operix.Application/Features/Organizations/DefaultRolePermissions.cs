using System.Collections.ObjectModel;
using Operix.Application.Features.Permissions;


namespace Operix.Application.Features.Organizations;


public static class DefaultRolePermissions
{
    public static IReadOnlyDictionary<string, IReadOnlyList<string>> Roles { get; } = new ReadOnlyDictionary<string, IReadOnlyList<string>>(
            new Dictionary<string, IReadOnlyList<string>>
            {
                ["Organization Administrator"] =
                [
                    PermissionCodes.OrganizationRead,
                    PermissionCodes.OrganizationUpdate,

                    PermissionCodes.UserRead,
                    PermissionCodes.UserCreate,
                    PermissionCodes.UserUpdate,
                    PermissionCodes.UserDelete,

                    PermissionCodes.RoleRead,
                    PermissionCodes.RoleCreate,
                    PermissionCodes.RoleUpdate,
                    PermissionCodes.RoleDelete,

                    PermissionCodes.DepartmentRead,
                    PermissionCodes.DepartmentCreate,
                    PermissionCodes.DepartmentUpdate,
                    PermissionCodes.DepartmentDelete,

                    PermissionCodes.LocationRead,
                    PermissionCodes.LocationCreate,
                    PermissionCodes.LocationUpdate,
                    PermissionCodes.LocationDelete
                ],

                ["Maintenance Manager"] =
                [
                    PermissionCodes.DepartmentRead,
                    PermissionCodes.DepartmentCreate,
                    PermissionCodes.DepartmentUpdate,
                    PermissionCodes.DepartmentDelete,

                    PermissionCodes.LocationRead,
                    PermissionCodes.LocationCreate,
                    PermissionCodes.LocationUpdate,
                    PermissionCodes.LocationDelete,

                    PermissionCodes.AssetRead,
                    PermissionCodes.AssetCreate,
                    PermissionCodes.AssetUpdate,
                    PermissionCodes.AssetDelete,

                    PermissionCodes.AssetCategoryRead,
                    PermissionCodes.AssetCategoryCreate,
                    PermissionCodes.AssetCategoryUpdate,
                    PermissionCodes.AssetCategoryDelete,

                    PermissionCodes.VendorRead,
                    PermissionCodes.VendorCreate,
                    PermissionCodes.VendorUpdate,
                    PermissionCodes.VendorDelete,

                    PermissionCodes.PreventiveMaintenanceRead,
                    PermissionCodes.PreventiveMaintenanceCreate,
                    PermissionCodes.PreventiveMaintenanceUpdate,
                    PermissionCodes.PreventiveMaintenanceDelete,

                    PermissionCodes.MaintenanceScheduleRead,
                    PermissionCodes.MaintenanceScheduleCreate,
                    PermissionCodes.MaintenanceScheduleUpdate,
                    PermissionCodes.MaintenanceScheduleDelete,

                    PermissionCodes.WorkOrderRead,
                    PermissionCodes.WorkOrderCreate,
                    PermissionCodes.WorkOrderUpdate,
                    PermissionCodes.WorkOrderDelete,
                    PermissionCodes.WorkOrderAssign,
                    PermissionCodes.WorkOrderComplete,

                    PermissionCodes.WorkOrderTaskRead,
                    PermissionCodes.WorkOrderTaskCreate,
                    PermissionCodes.WorkOrderTaskUpdate,
                    PermissionCodes.WorkOrderTaskDelete,

                    PermissionCodes.SparePartRead,
                    PermissionCodes.SparePartCreate,
                    PermissionCodes.SparePartUpdate,
                    PermissionCodes.SparePartDelete,

                    PermissionCodes.InventoryRead,
                    PermissionCodes.InventoryCreate,
                    PermissionCodes.InventoryUpdate,
                    PermissionCodes.InventoryDelete,

                    PermissionCodes.StockTransactionRead,
                    PermissionCodes.StockTransactionCreate
                ],

                ["Maintenance Supervisor"] =
                [
                    PermissionCodes.AssetRead,
                    PermissionCodes.AssetCategoryRead,
                    PermissionCodes.LocationRead,
                    PermissionCodes.VendorRead,

                    PermissionCodes.PreventiveMaintenanceRead,
                    PermissionCodes.PreventiveMaintenanceUpdate,

                    PermissionCodes.MaintenanceScheduleRead,
                    PermissionCodes.MaintenanceScheduleUpdate,

                    PermissionCodes.WorkOrderRead,
                    PermissionCodes.WorkOrderCreate,
                    PermissionCodes.WorkOrderUpdate,
                    PermissionCodes.WorkOrderAssign,
                    PermissionCodes.WorkOrderComplete,

                    PermissionCodes.WorkOrderTaskRead,
                    PermissionCodes.WorkOrderTaskCreate,
                    PermissionCodes.WorkOrderTaskUpdate,

                    PermissionCodes.SparePartRead,
                    PermissionCodes.InventoryRead,
                    PermissionCodes.StockTransactionRead
                ],

                ["Maintenance Technician"] =
                [
                    PermissionCodes.AssetRead,
                    PermissionCodes.LocationRead,
                    PermissionCodes.MaintenanceScheduleRead,

                    PermissionCodes.WorkOrderRead,
                    PermissionCodes.WorkOrderUpdate,
                    PermissionCodes.WorkOrderComplete,

                    PermissionCodes.WorkOrderTaskRead,
                    PermissionCodes.WorkOrderTaskUpdate,

                    PermissionCodes.SparePartRead,
                    PermissionCodes.InventoryRead
                ],

                ["Inventory Manager"] =
                [
                    PermissionCodes.SparePartRead,
                    PermissionCodes.SparePartCreate,
                    PermissionCodes.SparePartUpdate,
                    PermissionCodes.SparePartDelete,

                    PermissionCodes.InventoryRead,
                    PermissionCodes.InventoryCreate,
                    PermissionCodes.InventoryUpdate,
                    PermissionCodes.InventoryDelete,

                    PermissionCodes.StockTransactionRead,
                    PermissionCodes.StockTransactionCreate
                ],

                ["Requester"] =
                [
                    PermissionCodes.AssetRead,
                    PermissionCodes.WorkOrderRead,
                    PermissionCodes.WorkOrderCreate
                ]
            });
}
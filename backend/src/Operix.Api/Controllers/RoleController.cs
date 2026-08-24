using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operix.Api.Authorization;
using Operix.Application.Features.Permissions;
using Operix.Application.Features.RolePermissions;
using Operix.Application.Features.Roles;

namespace Operix.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/roles")]
public sealed class RolesController : ControllerBase
{
    private readonly RoleService _roleService;
    private readonly RolePermissionService _rolePermissionService;

    public RolesController(RoleService roleService, RolePermissionService rolePermissionService)
    {
        _roleService = roleService;
        _rolePermissionService = rolePermissionService;
    }

    [HttpPost]
    [HasPermission(PermissionCodes.RoleCreate)]
    public async Task<ActionResult<RoleDto>> Create(CreateRoleDto dto, CancellationToken cancellationToken)
    {
        var role = await _roleService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    [HttpGet("{id:int}")]
    [HasPermission(PermissionCodes.RoleRead)]
    public async Task<ActionResult<RoleDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetByIdAsync(id, cancellationToken);

        if (role is null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpGet]
    [HasPermission(PermissionCodes.RoleRead)]
    public async Task<ActionResult<IReadOnlyList<RoleDto>>> GetAll([FromQuery] int organizationId, CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllAsync(organizationId, cancellationToken);

        return Ok(roles);
    }

    [HttpPut("{id:int}")]
    [HasPermission(PermissionCodes.RoleUpdate)]
    public async Task<ActionResult<RoleDto>> Update(int id, UpdateRoleDto dto, CancellationToken cancellationToken)
    {
        var role = await _roleService.UpdateAsync(id, dto, cancellationToken);

        if (role is null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpDelete("{id:int}")]
    [HasPermission(PermissionCodes.RoleDelete)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _roleService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:int}/permissions")]
    [HasPermission(PermissionCodes.RoleUpdate)]
    public async Task<IActionResult> AssignPermissions(int id, AssignPermissionsDto dto, CancellationToken cancellationToken)
    {
        await _rolePermissionService.AssignPermissionsAsync(id, dto, cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:int}/permissions")]
    [HasPermission(PermissionCodes.RoleRead)]
    public async Task<ActionResult<IReadOnlyList<PermissionDto>>> GetPermissions(int id, CancellationToken cancellationToken)
    {
        var permissions = await _rolePermissionService.GetPermissionsAsync(id, cancellationToken);

        return Ok(permissions);
    }
}
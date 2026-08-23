using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operix.Api.Authorization;
using Operix.Application.Authorization;
using Operix.Application.DTOs;
using Operix.Application.Services;

namespace Operix.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/permissions")]
public sealed class PermissionsController : ControllerBase
{
    private readonly RolePermissionService _rolePermissionService;

    public PermissionsController(RolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }

    [HttpGet]
    [HasPermission(PermissionCodes.PermissionRead)]
    public async Task<ActionResult<IReadOnlyList<PermissionDto>>> GetAll(CancellationToken cancellationToken)
    {
        var permissions = await _rolePermissionService.GetAllPermissionsAsync(cancellationToken);

        return Ok(permissions);
    }
}
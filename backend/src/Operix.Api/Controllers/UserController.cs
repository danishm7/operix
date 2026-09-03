using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operix.Api.Authorization;
using Operix.Application.Features.Permissions;
using Operix.Application.Features.Users;

namespace Operix.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [HasPermission(PermissionCodes.UserCreate)]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:int}")]
    [HasPermission(PermissionCodes.UserRead)]
    public async Task<ActionResult<UserDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    [HasPermission(PermissionCodes.UserRead)]
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll([FromQuery] int organizationId, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(organizationId, cancellationToken);

        return Ok(users);
    }

    [HttpPut("{id:int}")]
    [HasPermission(PermissionCodes.UserUpdate)]
    public async Task<ActionResult<UserDto>> Update(int id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.UpdateAsync(id, dto, cancellationToken);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpDelete("{id:int}")]
    [HasPermission(PermissionCodes.UserDelete)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:int}/roles")]
    [HasPermission(PermissionCodes.UserUpdate)]
    public async Task<IActionResult> AssignRoles(int id, AssignRolesDto dto, CancellationToken cancellationToken)
    {
        await _userService.AssignRolesAsync(id, dto, cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:int}/roles")]
    [HasPermission(PermissionCodes.UserRead)]
    public async Task<IActionResult> GetRoles(int id, CancellationToken cancellationToken)
    {
        var roles = await _userService.GetRolesAsync(id, cancellationToken);

        return Ok(roles);
    }
}
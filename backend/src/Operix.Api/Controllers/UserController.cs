using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<UserDto>> Create(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:int}")]
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
    public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll([FromQuery] int organizationId, CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(organizationId, cancellationToken);

        return Ok(users);
    }

    [HttpPut("{id:int}")]
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
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
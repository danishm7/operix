using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operix.Api.Authorization;
using Operix.Application.Features.Organizations;
using Operix.Application.Features.Permissions;

namespace Operix.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/organizations")]
public sealed class OrganizationsController : ControllerBase
{
    private readonly OrganizationService _organizationService;

    public OrganizationsController(OrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpPost]
    [HasPermission(PermissionCodes.OrganizationCreate)]
    public async Task<ActionResult<OrganizationDto>> Create(CreateOrganizationDto dto, CancellationToken cancellationToken)
    {
        var organization = await _organizationService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(Create),
            new { id = organization.Id },
            organization);
    }

    [HttpGet("{id:int}")]
    [HasPermission(PermissionCodes.OrganizationRead)]
    public async Task<ActionResult<OrganizationDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var organization = await _organizationService.GetByIdAsync(id, cancellationToken);

        if (organization == null)
        {
            return NotFound();
        }

        return Ok(organization);
    }

    [HttpGet]
    [HasPermission(PermissionCodes.OrganizationRead)]
    public async Task<ActionResult<IReadOnlyList<OrganizationDto>>> GetAll(CancellationToken cancellationToken)
    {
        var organizations = await _organizationService.GetAllAsync(cancellationToken);

        return Ok(organizations);
    }

    [HttpPut("{id:int}")]
    [HasPermission(PermissionCodes.OrganizationUpdate)]
    public async Task<ActionResult<OrganizationDto>> Update(int id, UpdateOrganizationDto dto, CancellationToken cancellationToken)
    {
        var organization = await _organizationService.UpdateAsync(id, dto, cancellationToken);

        if (organization == null)
        {
            return NotFound();
        }

        return Ok(organization);
    }

    [HttpDelete("{id:int}")]
    [HasPermission(PermissionCodes.OrganizationUpdate)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _organizationService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
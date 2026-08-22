using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operix.Application.DTOs;
using Operix.Application.Services;

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
    public async Task<ActionResult<OrganizationDto>> Create(CreateOrganizationDto dto, CancellationToken cancellationToken)
    {
        var organization = await _organizationService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(
            nameof(Create),
            new { id = organization.Id },
            organization);
    }

    [HttpGet("{id:int}")]
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
    public async Task<ActionResult<IReadOnlyList<OrganizationDto>>> GetAll(CancellationToken cancellationToken)
    {
        var organizations = await _organizationService.GetAllAsync(cancellationToken);

        return Ok(organizations);
    }

    [HttpPut("{id:int}")]
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
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _organizationService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}